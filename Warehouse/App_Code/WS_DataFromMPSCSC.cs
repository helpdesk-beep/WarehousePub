using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
//using WS_DataFromMPSCSC;

/// <summary>
/// Summary description for WS_DataFromMPSCSC
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WS_DataFromMPSCSC : System.Web.Services.WebService
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    SqlCommand cmd = null;
    string TheResult = "";

    public WS_DataFromMPSCSC()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string GetMaxDateTime(string TableName)
    {
        string qry = "";
        if (TableName == "tbl_Receipt_Details")
        {
            qry = "select MAX(Created_date) from MPSCSC.dbo.[tbl_Receipt_Details]";
        }
        else if (TableName == "tbl_Receipt_Details_2019")
        {
            qry = "select MAX(Created_date) from MPSCSC.dbo.[tbl_Receipt_Details_2019]";
        }
        else if (TableName == "Acceptance_Note_kharif2023")
        {
            qry = "select MAX(Created_date) from MPSCSC.dbo.[Acceptance_Note_kharif2023]";
        }
        else if (TableName == "Acceptance_Note_kharif2023")
        {
            qry = "select MAX(Created_date) from MPSCSC.dbo.[Acceptance_Note_kharif2023]";
        }
        else if (TableName == "RR_receipt_Depot")
        {
            qry = "select MAX(Created_date) from MPSCSC.dbo.[RR_receipt_Depot]";
        }
        else if (TableName == "tbl_Digitally_Signed_Bill_RPO_SteelSilo_csms")
        {
            qry = "select MAX(CreatedDate_RAM) from [MPSCSC].[dbo].tbl_Digitally_Signed_Bill_RPO_SteelSilo_csms";
        }
        else if (TableName == "Digitally_Sign_StorageBill_IC")
        {
            qry = "select MAX(CreatedDate) from MPSCSC.dbo.Digitally_Sign_StorageBill_IC";
        }
        else if (TableName == "tbl_Storage_Payment_MPSCSC_NEFT")
        {
            qry = "select MAX(CreatedDate) from [MPSCSC].[dbo].tbl_Storage_Payment_MPSCSC_NEFT";
        }
        else if (TableName == "tbl_Processing_StorageBill_AtDM")
        {
            qry = "select MAX(CSMS_CreatedDate) from [MPSCSC].[dbo].[tbl_Processing_StorageBill_AtDM]";
        }
        else if (TableName == "tbl_Digital_Sign_StorageBill_Final_ForNeft")
        {
            qry = "select MAX(csms_CreatedDate) from [MPSCSC].[dbo].[tbl_Digital_Sign_StorageBill_Final_ForNeft] ";
        }
        else if (TableName == "StoragePayment_BillsFromCSMStoMPWLC_AfterAugust2020")
        {
            qry = "select MAX(BranchBillPaymentDate) from [MPSCSC].[dbo].[StoragePayment_BillsFromCSMStoMPWLC_AfterAugust2020]";
        }
        else if (TableName == "CMR_QualityInspection_2019")
        {
            qry = "select MAX(Current_DateTime) from [csms].[dbo].[CMR_QualityInspection_2019]";
        }
        else if (TableName == "CMR_QualityInspection_RemainingCMR_2019")
        {
            qry = "select MAX(Current_DateTime) from [csms].[dbo].[CMR_QualityInspection_RemainingCMR_2019]";
        }
        else
        {
            qry = "";
        }

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            TheResult = ds.Tables[0].Rows[0]["Column1"].ToString();
        }
        return TheResult;
    }

}
