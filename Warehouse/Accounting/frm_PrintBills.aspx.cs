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
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Security.Principal;

public partial class Accounting_frm_PrintBills : System.Web.UI.Page
{
    string Bill_Number = "";
    string Bill_Type = "";
    string NetAmountWord = "";
    string NetAmount = "";
     
    public string query="";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["BranchID"] != null))
        {
            if (!IsPostBack)
            {
                GetBillNumbers();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtBillNo.Text == "" || txtBillNo.Text == null)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Bill Number'); </script> ");
            trReportsView.Visible = false;
        }
        else
        {
            Bill_Number = txtBillNo.Text;
            GetBillType(Bill_Number);
            if (Bill_Type != "" && Bill_Type != null)
            {
                if (Bill_Type == "AD")
                {
                    Report_Storage_Bill_Daily_Details();
                }
                else if (Bill_Type == "AU")
                {
                    Accrued_Storage_Bill_Details();
                }
                else if (Bill_Type == "RB")
                {
                    Report_MCReservation_Bill();
                }
                else if (Bill_Type == "OD")
                {
                    Report_Over_N_Above_Bill_Details();
                }
                else if (Bill_Type == "OU")
                {
                    Report_Accrued_Over_N_Above_Bill();
                }
                else if (Bill_Type == "GR")
                {
                    Accrued_Storage_Bill_Details();
                }
                else if (Bill_Type == "HG")
                {
                    Accrued_Storage_Bill_Details();
                }
                else if (Bill_Type == "FD")
                {
                    Report_Nafed_Bill();
                }
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Bill Number Does Not Exist...'); </script> ");
                trReportsView.Visible = false;
            }
        }
    }
    public void GetBillNumbers()
    {
        string BranchID = Session["BranchID"].ToString();
        query = "select Bill_Number from tbl_Storage_Bill_Details where Branch_Id='" + BranchID + "'";
        cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBillNo.DataSource = ds.Tables[0];
            ddlBillNo.DataTextField = "Bill_Number";
            ddlBillNo.DataValueField = "Bill_Number";
            ddlBillNo.DataBind();
            ddlBillNo.Items.Insert(0,"--Select--");

        }
    }
    public void GetBillType(string BN)
    {
            query = "select Bill_Type,Net_Amount,(select dbo.AnkNumberToWords(Net_Amount)) as NetAmountWord from tbl_Storage_Bill_Details where Bill_Number='" + Bill_Number + "'";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Bill_Type = ds.Tables[0].Rows[0]["Bill_Type"].ToString();
                NetAmount = ds.Tables[0].Rows[0]["Net_Amount"].ToString();
                NetAmountWord = ds.Tables[0].Rows[0]["NetAmountWord"].ToString();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Bill No Does't Exist...'); </script> ");
                trReportsView.Visible = false;
            }
    }
    public void Report_Storage_Bill_Daily_Details()
    {
        string BranchID = Session["BranchID"].ToString();
        trReportsView.Visible = true;
        rvPrintBill.Visible = false;
        rvPrintBill.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        try
        {
            string uname = "";
            string pwd = "";
            string domain = "";
            string reportURL = "";
            string folder = "";
            string serverFullName = null;
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

            rvPrintBill.ServerReport.ReportServerUrl = new Uri(servername.ToString());
            rvPrintBill.Visible = true;
            uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
            pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
            domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
            reportURL = "";
            folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
            reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
            rvPrintBill.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rvPrintBill.ServerReport.ReportServerUrl = new Uri(reportURL);

            //Bill_No = ViewState["BillNo"].ToString();
            //decimal Comm_Rate = Convert.ToDecimal(txtcomrate.Text);
            //ChargeOfTotal = Convert.ToDecimal(ViewState["ChargeOfTotal"]);
            if (ddlBType.SelectedValue.ToString() == "B" && ddlBType.SelectedValue.ToString() != "-1")
            {
                rvPrintBill.ServerReport.ReportPath = folder + "/" + "rptStorageChargesDailyBill";
            }
            else if (ddlBType.SelectedValue.ToString() == "W" && ddlBType.SelectedValue.ToString() != "-1")
            {
                rvPrintBill.ServerReport.ReportPath = folder + "/" + "Bill_InstitutionDailyChargesOnMT";
            }
            rvPrintBill.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
            rvPrintBill.ShowCredentialPrompts = false;
            ReportParameter[] reportParameterCollection = new ReportParameter[3];
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "Branch_Id";
            reportParameterCollection[0].Values.Add(BranchID);
            reportParameterCollection[1] = new ReportParameter();
            reportParameterCollection[1].Name = "Bill_No";
            reportParameterCollection[1].Values.Add(Bill_Number);
            reportParameterCollection[2] = new ReportParameter();
            reportParameterCollection[2].Name = "NetAmountWord";
            reportParameterCollection[2].Values.Add(NetAmountWord);
            rvPrintBill.ServerReport.SetParameters(reportParameterCollection);
            rvPrintBill.ServerReport.Refresh();

        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }
    public void Accrued_Storage_Bill_Details()
    {
        string BranchID = Session["BranchID"].ToString();
        trReportsView.Visible = true;
        rvPrintBill.Visible = false;
        rvPrintBill.Visible = false;
        rvPrintBill.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        try
        {
            string uname = "";
            string pwd = "";
            string domain = "";
            string reportURL = "";
            string folder = "";
            string serverFullName = null;
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

            rvPrintBill.ServerReport.ReportServerUrl = new Uri(servername.ToString());
            rvPrintBill.Visible = true;
            uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
            pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
            domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
            reportURL = "";
            folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
            reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
            rvPrintBill.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rvPrintBill.ServerReport.ReportServerUrl = new Uri(reportURL);

            //NetAmountWord = (Convert_To_Word(NetAmount)).ToString();
            //Bill_No = ViewState["BillNo"].ToString();

            rvPrintBill.ServerReport.ReportPath = folder + "/" + "rptAccruedStorageCharges";
            rvPrintBill.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
            rvPrintBill.ShowCredentialPrompts = false;
            ReportParameter[] reportParameterCollection = new ReportParameter[3];
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "Branch_Id";
            reportParameterCollection[0].Values.Add(BranchID);
            reportParameterCollection[1] = new ReportParameter();
            reportParameterCollection[1].Name = "Bill_No";
            reportParameterCollection[1].Values.Add(Bill_Number);

            reportParameterCollection[2] = new ReportParameter();
            reportParameterCollection[2].Name = "NetAmountWord";
            reportParameterCollection[2].Values.Add(NetAmountWord);

            rvPrintBill.ServerReport.SetParameters(reportParameterCollection);
            rvPrintBill.ServerReport.Refresh();

        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }
    public void Report_Over_N_Above_Bill_Details()
    {
        string BranchID = Session["BranchID"].ToString();
        trReportsView.Visible = true;
        rvPrintBill.Visible = false;
        rvPrintBill.Visible = false;
        rvPrintBill.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        try
        {
            string uname = "";
            string pwd = "";
            string domain = "";
            string reportURL = "";
            string folder = "";
            string serverFullName = null;
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

            rvPrintBill.ServerReport.ReportServerUrl = new Uri(servername.ToString());
            rvPrintBill.Visible = true;
            uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
            pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
            domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
            reportURL = "";
            folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
            reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
            rvPrintBill.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rvPrintBill.ServerReport.ReportServerUrl = new Uri(reportURL);

            //Bill_No = ViewState["BillNo"].ToString();
            //decimal Comm_Rate = Convert.ToDecimal(txtcomrate.Text);
            //ChargeOfTotal = Convert.ToDecimal(ViewState["ChargeOfTotal"]);
            rvPrintBill.ServerReport.ReportPath = folder + "/" + "rptOverNAboveDailyBill";
            rvPrintBill.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
            rvPrintBill.ShowCredentialPrompts = false;
            ReportParameter[] reportParameterCollection = new ReportParameter[3];
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "Branch_Id";
            reportParameterCollection[0].Values.Add(BranchID);
            reportParameterCollection[1] = new ReportParameter();
            reportParameterCollection[1].Name = "Bill_No";
            reportParameterCollection[1].Values.Add(Bill_Number);
            reportParameterCollection[2] = new ReportParameter();
            reportParameterCollection[2].Name = "NetAmountWord";
            reportParameterCollection[2].Values.Add(NetAmountWord);
            rvPrintBill.ServerReport.SetParameters(reportParameterCollection);
            rvPrintBill.ServerReport.Refresh();

        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }
    public void Report_Accrued_Over_N_Above_Bill()
    {
        string BranchID = Session["BranchID"].ToString();
        trReportsView.Visible = true;
        rvPrintBill.Visible = false;
        rvPrintBill.Visible = false;
        rvPrintBill.Visible = false;
        rvPrintBill.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        try
        {
            string uname = "";
            string pwd = "";
            string domain = "";
            string reportURL = "";
            string folder = "";
            string serverFullName = null;
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

            rvPrintBill.ServerReport.ReportServerUrl = new Uri(servername.ToString());
            rvPrintBill.Visible = true;
            uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
            pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
            domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
            reportURL = "";
            folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
            reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
            rvPrintBill.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rvPrintBill.ServerReport.ReportServerUrl = new Uri(reportURL);

            //Bill_No = ViewState["BillNo"].ToString();
            //decimal Comm_Rate = Convert.ToDecimal(txtcomrate.Text);
            //rvPrintBill = Convert.ToDecimal(ViewState["ChargeOfTotal"]);
            rvPrintBill.ServerReport.ReportPath = folder + "/" + "rptOverNAboveAccruedBills";
            rvPrintBill.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
            rvPrintBill.ShowCredentialPrompts = false;
            ReportParameter[] reportParameterCollection = new ReportParameter[3];
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "Branch_Id";
            reportParameterCollection[0].Values.Add(BranchID);
            reportParameterCollection[1] = new ReportParameter();
            reportParameterCollection[1].Name = "Bill_No";
            reportParameterCollection[1].Values.Add(Bill_Number);
            reportParameterCollection[2] = new ReportParameter();
            reportParameterCollection[2].Name = "NetAmountWord";
            reportParameterCollection[2].Values.Add(NetAmountWord);
            rvPrintBill.ServerReport.SetParameters(reportParameterCollection);
            rvPrintBill.ServerReport.Refresh();

        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }
    public void Report_MCReservation_Bill()
    {
        try
        {
            //Bill_No = ViewState["BillNo"].ToString();
            //Get_Reservation_NetAmt();
            trReportsView.Visible = true;
            rvPrintBill.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;

            string uname = "";
            string pwd = "";
            string domain = "";
            string reportURL = "";
            string folder = "";
            string serverFullName = null;
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

            rvPrintBill.ServerReport.ReportServerUrl = new Uri(servername.ToString());
            rvPrintBill.Visible = true;
            uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
            pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
            domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
            reportURL = "";
            folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
            reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
            rvPrintBill.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rvPrintBill.ServerReport.ReportServerUrl = new Uri(reportURL);
            rvPrintBill.ServerReport.ReportPath = folder + "/" + "rptMCReservationBillCharges";
            rvPrintBill.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
            rvPrintBill.ShowCredentialPrompts = false;
            ReportParameter[] reportParameterCollection = new ReportParameter[3];
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "NetAmountWord";
            reportParameterCollection[0].Values.Add(NetAmountWord);
            reportParameterCollection[1] = new ReportParameter();
            reportParameterCollection[1].Name = "Bill_No";
            reportParameterCollection[1].Values.Add(Bill_Number);
            reportParameterCollection[2] = new ReportParameter();
            reportParameterCollection[2].Name = "NetAmount";
            reportParameterCollection[2].Values.Add(NetAmount.ToString());
            rvPrintBill.ServerReport.SetParameters(reportParameterCollection);
            rvPrintBill.ServerReport.Refresh();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }
    public void Report_Godown_Bill_Daily_Details()
    {
        //string MMYY = ddlmonth.SelectedItem.Text + "-" + ddlFyear.SelectedItem.Text;
        string BranchID = Session["BranchID"].ToString();
        trReportsView.Visible = true;
        rvPrintBill.Visible = false;
        rvPrintBill.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        try
        {
            string uname = "";
            string pwd = "";
            string domain = "";
            string reportURL = "";
            string folder = "";
            string serverFullName = null;
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

            rvPrintBill.ServerReport.ReportServerUrl = new Uri(servername.ToString());
            rvPrintBill.Visible = true;
            uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
            pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
            domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
            reportURL = "";
            folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
            reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
            rvPrintBill.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rvPrintBill.ServerReport.ReportServerUrl = new Uri(reportURL);

            //Bill_No = ViewState["BillNo"].ToString();
            //decimal Comm_Rate = Convert.ToDecimal(txtcomrate.Text);
            //ChargeOfTotal = Convert.ToDecimal(ViewState["ChargeOfTotal"]);
            NetAmountWord = "  ";
            rvPrintBill.ServerReport.ReportPath = folder + "/" + "rptGodown(JV)_Rent_Storage_Bill";
            rvPrintBill.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
            rvPrintBill.ShowCredentialPrompts = false;
            ReportParameter[] reportParameterCollection = new ReportParameter[3];
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "Branch_Id";
            reportParameterCollection[0].Values.Add(BranchID);
            reportParameterCollection[1] = new ReportParameter();
            reportParameterCollection[1].Name = "Bill_No";
            reportParameterCollection[1].Values.Add(Bill_Number);
            reportParameterCollection[2] = new ReportParameter();
            reportParameterCollection[2].Name = "NetAmountWord";
            reportParameterCollection[2].Values.Add(NetAmountWord);
            //reportParameterCollection[3] = new ReportParameter();
            //reportParameterCollection[3].Name = "Month";
            //reportParameterCollection[3].Values.Add(MMYY);
            rvPrintBill.ServerReport.SetParameters(reportParameterCollection);
            rvPrintBill.ServerReport.Refresh();

        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }
    public void Report_Godown_Hired_Bill()
    {
        //string MMYY = ddlmonth.SelectedItem.Text + "-" + ddlFyear.SelectedItem.Text;
        //NetAmountWord = Convert_To_Word(Convert.ToDecimal(txtRent.Text));
        string BranchID = Session["BranchID"].ToString();
        trReportsView.Visible = true;
        //trRentBill.Visible = false;
        rvPrintBill.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        try
        {
            string uname = "";
            string pwd = "";
            string domain = "";
            string reportURL = "";
            string folder = "";
            string serverFullName = null;
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

            rvPrintBill.ServerReport.ReportServerUrl = new Uri(servername.ToString());
            rvPrintBill.Visible = true;
            uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
            pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
            domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
            reportURL = "";
            folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
            reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
            rvPrintBill.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rvPrintBill.ServerReport.ReportServerUrl = new Uri(reportURL);

            //Bill_No = ViewState["BillNo"].ToString();
            //decimal Comm_Rate = Convert.ToDecimal(txtcomrate.Text);
            //ChargeOfTotal = Convert.ToDecimal(ViewState["ChargeOfTotal"]);
            //NetAmountWord = Convert_To_Word(Total_Charge);
            rvPrintBill.ServerReport.ReportPath = folder + "/" + "rptGodown_Hired_Rent_Bill";
            rvPrintBill.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
            rvPrintBill.ShowCredentialPrompts = false;
            ReportParameter[] reportParameterCollection = new ReportParameter[3];
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "Branch_Id";
            reportParameterCollection[0].Values.Add(BranchID);
            reportParameterCollection[1] = new ReportParameter();
            reportParameterCollection[1].Name = "Bill_No";
            reportParameterCollection[1].Values.Add(Bill_Number);
            reportParameterCollection[2] = new ReportParameter();
            reportParameterCollection[2].Name = "NetAmountWord";
            reportParameterCollection[2].Values.Add(NetAmountWord);
            rvPrintBill.ServerReport.SetParameters(reportParameterCollection);
            rvPrintBill.ServerReport.Refresh();

        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }
    public void Report_Nafed_Bill()
    {
        string BranchID = Session["BranchID"].ToString();
        trReportsView.Visible = true;
        rvPrintBill.Visible = false;
        rvPrintBill.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        try
        {
            string uname = "";
            string pwd = "";
            string domain = "";
            string reportURL = "";
            string folder = "";
            string serverFullName = null;
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

            rvPrintBill.ServerReport.ReportServerUrl = new Uri(servername.ToString());
            rvPrintBill.Visible = true;
            uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
            pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
            domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
            reportURL = "";
            folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
            reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
            rvPrintBill.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rvPrintBill.ServerReport.ReportServerUrl = new Uri(reportURL);

            //Bill_No = ViewState["BillNo"].ToString();
            //decimal Comm_Rate = Convert.ToDecimal(txtcomrate.Text);
            //ChargeOfTotal = Convert.ToDecimal(ViewState["ChargeOfTotal"]);

            rvPrintBill.ServerReport.ReportPath = folder + "/" + "Bill_NafedStorageCharges";
          
            rvPrintBill.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
            rvPrintBill.ShowCredentialPrompts = false;
            ReportParameter[] reportParameterCollection = new ReportParameter[3];
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "Branch_Id";
            reportParameterCollection[0].Values.Add(BranchID);
            reportParameterCollection[1] = new ReportParameter();
            reportParameterCollection[1].Name = "Bill_No";
            reportParameterCollection[1].Values.Add(Bill_Number);
            reportParameterCollection[2] = new ReportParameter();
            reportParameterCollection[2].Name = "NetAmountWord";
            reportParameterCollection[2].Values.Add(NetAmountWord);
            rvPrintBill.ServerReport.SetParameters(reportParameterCollection);
            rvPrintBill.ServerReport.Refresh();

        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
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
    protected void ddlBillNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bill_Number = ddlBillNo.SelectedValue;
        GetBillType(Bill_Number);
        if (Bill_Type == "AD")
        {
            Report_Storage_Bill_Daily_Details();
        }
        else if (Bill_Type == "AU")
        {
            Accrued_Storage_Bill_Details();
        }
        else if (Bill_Type == "RB")
        {
            Report_MCReservation_Bill();
        }
        else if (Bill_Type == "OD")
        {
            Report_Over_N_Above_Bill_Details();
        }
        else if (Bill_Type == "OU")
        {
            Report_Accrued_Over_N_Above_Bill();
        }
        else if (Bill_Type == "GR")
        {
            Report_Godown_Bill_Daily_Details();
        }
        else if (Bill_Type == "HG")
        {
            Report_Godown_Hired_Bill();
        }
        else if (Bill_Type == "FD")
        {
            Report_Nafed_Bill();
        }
    }
}
