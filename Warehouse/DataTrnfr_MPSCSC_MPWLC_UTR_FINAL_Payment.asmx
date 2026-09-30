<%@ WebService Language="C#" Class="DataTrnfr_MPSCSC_MPWLC_UTR_FINAL_Payment" %>


using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;

/// <summary>
/// Summary description 
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class DataTrnfr_MPSCSC_MPWLC_UTR_FINAL_Payment : System.Web.Services.WebService
{

    SqlConnection con_MP = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);//MPSCSC
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_Rabi2022"].ConnectionString);//CSMSRMS2022

    public DataTrnfr_MPSCSC_MPWLC_UTR_FINAL_Payment()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod]
    public DataSet get_StoragePaymentFull(string USERID, string Password)
    {

        SqlDataAdapter da;
        DataSet DistDs = new DataSet();
        if (USERID == "eup2021_WebApp" && Password == "$#KHeup2021")
        {
            SqlCommand cmd = new SqlCommand();
            string str = "SELECT [LotNo],[Bill_Number],[Ref_Bill_No],[District_Id],[District_Name],[Branch_Id],[Branch_Name],[ComponentNo],[Godown_ID],[Godown_Name],[Acc_Holder_Name],[Account_No],[IFSC_Code],[Gross_Amount],[TDS_Amt],[OtherDeduction],[Payable_Amount],[Crop_Year],[Financial_Year],[Month],[Commodity_Name],[PaymentPushedDate],[BranchBill_BankUTRNo],[BranchBillPaymentDate] FROM StoragePayment_BillsFromCSMStoMPWLC_AfterAugust2020 ";

            da = new SqlDataAdapter(str, con_MP);
            DistDs = new DataSet();
            da.Fill(DistDs);
        }
        else
        {
            DistDs = null;
        }
        return DistDs;

    }





    [WebMethod]
    public DataSet GetStoragePayment_Distid_LotId(string USERID, string Password, string District_Id, string LotID)
    {

        SqlDataAdapter da;
        DataSet DistDs = new DataSet();
        if (USERID == "eup2021_WebApp" && Password == "$#KHeup2021")
        {
            SqlCommand cmd = new SqlCommand();
            string str = "SELECT [LotNo],[Bill_Number],[Ref_Bill_No],[District_Id],[District_Name],[Branch_Id],[Branch_Name],[ComponentNo],[Godown_ID],[Godown_Name],[Acc_Holder_Name],[Account_No],[IFSC_Code],[Gross_Amount],[TDS_Amt],[OtherDeduction],[Payable_Amount],[Crop_Year],[Financial_Year],[Month],[Commodity_Name],[PaymentPushedDate],[BranchBill_BankUTRNo],[BranchBillPaymentDate] FROM StoragePayment_BillsFromCSMStoMPWLC_AfterAugust2020 WHERE District_Id = '" + District_Id + "' AND LotNo = '" + LotID + "' ";

            da = new SqlDataAdapter(str, con_MP);
            DistDs = new DataSet();
            da.Fill(DistDs);
        }
        else
        {
            DistDs = null;
        }
        return DistDs;

    }

    [WebMethod]

    public DataSet PostStoragePayment_DataWLC(string UserID, string Password, string LotNo, string Bill_Number, string Ref_Bill_No, string District_Id, string District_Name, string Branch_Id, string Branch_Name, string ComponentNo, string Godown_ID, string Godown_Name, string Acc_Holder_Name, string Account_No, string IFSC_Code, string Gross_Amount, string TDS_Amt, string OtherDeduction, string Payable_Amount, string Crop_Year, string Financial_Year, string Month, string Commodity_Name, string PaymentPushedDate, string BranchBill_BankUTRNo, string BranchBillPaymentDate)
    {


        DataSet Ds1 = new DataSet();
        if (UserID == "eup2021_WebApp" && Password == "$#KHeup2021")
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da;
            DataSet Ds = new DataSet();

            string str = "SELECT [LotNo],[Bill_Number],[Ref_Bill_No],[District_Id],[District_Name],[Branch_Id],[Branch_Name],[ComponentNo],[Godown_ID],[Godown_Name],[Acc_Holder_Name],[Account_No],[IFSC_Code],[Gross_Amount],[TDS_Amt],[OtherDeduction],[Payable_Amount],[Crop_Year],[Financial_Year],[Month],[Commodity_Name],[PaymentPushedDate],[BranchBill_BankUTRNo],[BranchBillPaymentDate] FROM [MPSCSC].dbo.StoragePayment_BillsFromCSMStoMPWLC_AfterAugust2020_test WHERE District_Id = '" + District_Id + "' AND LotNo = '" + LotNo + "'";

            da = new SqlDataAdapter(str, con_MP);
            Ds = new DataSet();
            da.Fill(Ds);




            if (Ds.Tables[0].Rows.Count >= 0)
            {



                string str1 = "insert into [MPSCSC].dbo.[StoragePayment_BillsFromCSMStoMPWLC_AfterAugust2020_test]   ([LotNo],[Bill_Number],[Ref_Bill_No],[District_Id],[District_Name],[Branch_Id],[Branch_Name],[ComponentNo],[Godown_ID],[Godown_Name],[Acc_Holder_Name],[Account_No],[IFSC_Code],[Gross_Amount],[TDS_Amt],[OtherDeduction],[Payable_Amount],[Crop_Year],[Financial_Year],[Month],[Commodity_Name],[PaymentPushedDate],[BranchBill_BankUTRNo],[BranchBillPaymentDate])values('" + LotNo + "','" + Bill_Number + "','" + Ref_Bill_No + "','" + District_Id + "','" + District_Name + "','" + Branch_Id + "','" + Branch_Name + "','" + ComponentNo + "','" + Godown_ID + "','" + Godown_Name + "','" + Acc_Holder_Name + "','" + Account_No + "','" + IFSC_Code + "','" + Gross_Amount + "','" + TDS_Amt + "','" + OtherDeduction + "','" + Payable_Amount + "','" + Crop_Year + "','" + Financial_Year + "','" + Month + "','" + Commodity_Name + "','" + PaymentPushedDate + "','" + BranchBill_BankUTRNo + "','" + BranchBillPaymentDate + "') ";

                da = new SqlDataAdapter(str1, con_MP);
                Ds1 = new DataSet();
                da.Fill(Ds1);


            }
            else
            {

            }
        }
        else
        {
            Ds1 = null;
        }
        return Ds1;
    }




}
