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

public partial class IssueCenterLevel_Storage_ReportViewer_Depot : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            if (!IsPostBack)
            {
                GetDistricts();
                GetDepot();
                GetTodaydate();
            }
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
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        GetDeliveryorder();
    }

    private void GetDistricts()
    {
        try
        {
            string qry = "SELECT District_Id, District_Name FROM  tbl_MetaData_DISTRICT ORDER BY District_Name ";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_District.DataSource = ds.Tables[0];
                ddl_District.DataTextField = "District_Name";
                ddl_District.DataValueField = "District_Id";
                ddl_District.DataBind();
                ddl_District.Items.Insert(0, "--Select--");
                ddl_District.SelectedValue = Session["Depot_DistID"].ToString();
                ddl_District.Enabled = false;
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    private void GetDepot()
    {
        try
        {
            string qry = "SELECT BranchId, DepotName FROM  tbl_MetaData_DEPOT WHERE DistrictId = '" + Session["Depot_DistID"].ToString() + "' ORDER BY DepotName";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_Depot.DataSource = ds.Tables[0];
                ddl_Depot.DataTextField = "DepotName";
                ddl_Depot.DataValueField = "BranchId";
                ddl_Depot.DataBind();
                ddl_Depot.Items.Insert(0, "--Select--");
                // ddl_Depot.SelectedValue = Session["Depot_DepotID"].ToString();
                ddl_Depot.SelectedValue = Session["G_BranchId"].ToString();
                ddl_Depot.Enabled = false;
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    private void GetTodaydate()
    {
        try
        {
            string qry = "select  convert(varchar(10), getdate(),103) as TodayDate";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txt_to.Text = ds.Tables[0].Rows[0]["TodayDate"].ToString();
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    private void GetDeliveryorder()
    {
        try
        {
            //string qry = "select distinct  StockDeliveryOrder_Id,delivery_Order_no from tbl_Storage_Final_Stock_Delivery_Order where district_id='" + ddl_District.SelectedValue.ToString() + "' and depotid='" + ddl_Depot.SelectedValue.ToString() + "' and tbl_Storage_Final_Stock_Delivery_Order.Delivery_Order_Date >= Convert(Datetime, '" + txt_From.Text.Trim().ToString() + "', 104) and tbl_Storage_Final_Stock_Delivery_Order.Delivery_Order_Date <= Convert(Datetime, '" + txt_to.Text.Trim().ToString() + "', 104)";
            string qry = "select distinct  StockDeliveryOrder_Id,delivery_Order_no from tbl_Storage_Final_Stock_Delivery_Order where district_id='" + ddl_District.SelectedValue.ToString() + "' and BranchID='" + ddl_Depot.SelectedValue.ToString() + "' and tbl_Storage_Final_Stock_Delivery_Order.Delivery_Order_Date >= Convert(Datetime, '" + txt_From.Text.Trim().ToString() + "', 104) and tbl_Storage_Final_Stock_Delivery_Order.Delivery_Order_Date <= Convert(Datetime, '" + txt_to.Text.Trim().ToString() + "', 104)";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_do_no.DataSource = ds.Tables[0];
                ddl_do_no.DataTextField = "delivery_Order_no";
                ddl_do_no.DataValueField = "StockDeliveryOrder_Id";
                ddl_do_no.DataBind();
                ddl_do_no.Items.Insert(0, "--Select--");

            }
        }
        catch (Exception)
        {
            //////
        }
    }

    protected void ddl_do_no_SelectedIndexChanged(object sender, EventArgs e)
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
                Session["Depot_DistID"] = ddl_District.SelectedValue.ToString();
                Session["Depot_DepotID"] = ddl_Depot.SelectedValue.ToString();
            }
            else
            {
                //////
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

            if (url == "Rpt_Delivery_Order_New")
            {
                ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                ReportViewer_Depot.ShowCredentialPrompts = false;
                ReportParameter[] reportParameterCollection = new ReportParameter[1];
                //reportParameterCollection[0] = new ReportParameter();
                //reportParameterCollection[0].Name = "DistrictId";
                //reportParameterCollection[0].Values.Add(ddl_District.SelectedValue.ToString());
                //reportParameterCollection[1] = new ReportParameter();
                //reportParameterCollection[1].Name = "Depot";
                //reportParameterCollection[1].Values.Add(ddl_Depot.SelectedValue.ToString());
                //reportParameterCollection[2] = new ReportParameter();
                //reportParameterCollection[2].Name = "Language";
                //reportParameterCollection[2].Values.Add(ddl_Language.SelectedValue.ToString());
                //reportParameterCollection[3] = new ReportParameter();
                //reportParameterCollection[3].Name = "DelNo";
                //reportParameterCollection[3].Values.Add(ddl_do_no.SelectedItem.Text.ToString());
                reportParameterCollection[0] = new ReportParameter();
                reportParameterCollection[0].Name = "StockDeliveryOrder_Id";
                reportParameterCollection[0].Values.Add(ddl_do_no.SelectedValue.ToString());
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

