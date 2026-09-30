using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public partial class DataImportToMPSCSC : System.Web.UI.Page
{
   // public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conMPSCSC = new SqlConnection(ConfigurationManager.ConnectionStrings["MPSCSC"].ToString());

    SqlTransaction sqltrans;
    SqlCommand cmd = null;
    string TheResult = "";
    string TableName = "";
    //WSForMPSCSCData.WS_DataFromMPSCSC WSForData = new WSForMPSCSCData.WS_DataFromMPSCSC();
    //WSForMPSCSCData_Local.WS_DataFromMPSCSC WSForDataLocal = new WSForMPSCSCData_Local.WS_DataFromMPSCSC();


    protected void Page_Load(object sender, EventArgs e)
    {

    }

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
        else if (TableName == "tbl_MetaData_STORAGE_COMMODITY")
        {
            qry = "select max(CreatedDate) from [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_STORAGE_COMMODITY]";
        }
        else
        {
            qry = "";

        }

        SqlCommand cmd = new SqlCommand(qry, conMPSCSC);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {

        }
        return "";
    }

    protected void btnImportCommodity_Click(object sender, EventArgs e)
    {
        //if (con.State == ConnectionState.Closed)
        //{
        //    con.Open();
        //}
        int CommodityID=1000; 
        //string query = "select max(Commodity_Id)+1 from [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_STORAGE_COMMODITY]";
        //SqlCommand cmd = new SqlCommand(query, con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //CommodityID = Convert.ToInt32(cmd.ExecuteScalar());

        string InsertQuery = "INSERT INTO [Integrated_MP_STORAGE].[dbo].[tbl_MetaData_STORAGE_COMMODITY] (Commodity_Id, Commodity_Name, CreatedDate, Status, Rep_Grp_Code) VALUES" +
             "(" + CommodityID + ",'" + txtCommodityName.Text.ToString() + "', GETDATE(),'" + txtStatus.Text.ToString() + "'," + Convert.ToInt32(txtRepGroupCode.Text) + ")";


        //if (conMPSCSC.State == ConnectionState.Closed)
        //{
        //    conMPSCSC.Open();
        //}

        conMPSCSC.Open();
        SqlCommand cmdInsert = new SqlCommand(InsertQuery, conMPSCSC);
        cmdInsert.ExecuteNonQuery();
        conMPSCSC.Close();

        //if (conMPSCSC.State == ConnectionState.Closed)
        //{
        //    conMPSCSC.Open();
        //}
        //SqlCommand cmdInsertMPSCSC = new SqlCommand(InsertQuery, conMPSCSC);
        //cmdInsertMPSCSC.ExecuteNonQuery();


        //conMPSCSC.Close();
        //SqlDataAdapter daInsert = new SqlDataAdapter(cmdInsert);



    }
}
