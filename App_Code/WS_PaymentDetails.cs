using System;
using System.Activities.Expressions;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services.Configuration;

/// <summary>
/// Summary description for WS_PaymentDetails
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WS_PaymentDetails : System.Web.Services.WebService
{
    String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
    
    public class RentBillPaymentDetails
    {
        public string StorageBillNumber;
        public string RentBillNumber;
        public decimal GrossAmount;
        public decimal TDS;
        public decimal OtherDeduction;
        //public decimal PayableAmount;
        public decimal ReceivedAmountFromMPSCSC;
        //public decimal RentBillAmount;
        public decimal RentBillAmountPaidToGodown;
        public string CropYear;
        public string FinancialYear;
        public string Month;
        public string Commodity;

    }

    public class BeneficiaryAccountDetails
    {
        public string RegionID;
        public string RegionName;
        public string GodownOwnerAccountDetails;
        public string BeneficiaryName;
        public string IFSCCode;
        public string BeneficiaryMobile;
        public string BeneficiaryEmail;

    }

    public WS_PaymentDetails()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //[WebMethod]
    //[ScriptMethod(UseHttpGet = true)]
    //public void GetRentBillDetails(string RentBillNumber)
    //{
    //        string PaymentDatainXML;
    //        List<RentBillPaymentDetails> listRentBillPaymentDetails = new List<RentBillPaymentDetails>();
    //        SqlDataAdapter da = new SqlDataAdapter();
    //        SqlConnection con = new SqlConnection(str);
    //        con.Open();
    //        string qry = "SELECT GRA.Ref_Bill_No as [StorageBillNumber], GRA.Bill_No as [RentBillNumber], SPB.Gross_Amount as [GrossAmount],SPB.TDS_Amt as [TDS]" +
    //        "," + "SPB.OtherDeduction as [OtherDeduction], SPB.Payable_Amount as [PayableAmount], GRA.Net_Bill_Amount as [RentBillAmount], SPB.Crop_Year as [CropYear], SPB.Financial_Year as [FinancialYear]" +
    //        "," + "SPB.Month as [Month], SPB.Commodity_Name as [Commodity] FROM StoragePayment_BillsFromCSMStoMPWLC_AfterAugust2020 SPB INNER JOIN " +
    //        ""  + " tbl_Godown_Rent_Deduction_Amount GRA ON SPB.Bill_Number = GRA.Ref_Bill_No WHERE GRA.Bill_No ='"+ @RentBillNumber + "'";
    //        SqlCommand cmd = new SqlCommand(qry, con);
    //        cmd.ExecuteNonQuery();
    //        DataTable dt = new DataTable();
    //        DataSet ds = new DataSet();
    //        //da.Fill(dt);
    //        //ds.Tables.Add(dt);
    //        //ds.GetXml();
    //        //PaymentDatainXML = ds.GetXml().ToString();
    //        cmd.CommandTimeout = 0;
    //        SqlDataReader dr = cmd.ExecuteReader();
    //        while (dr.Read())
    //        {
    
    //            RentBillPaymentDetails objRBPD = new RentBillPaymentDetails();
    //            objRBPD.StorageBillNumber = Convert.ToString(dr["StorageBillNumber"] == DBNull.Value ? String.Empty : dr["StorageBillNumber"]);
    //            objRBPD.RentBillNumber = Convert.ToString(dr["RentBillNumber"] == DBNull.Value ? String.Empty : dr["RentBillNumber"]);
    //            objRBPD.GrossAmount = Convert.ToDecimal(dr["GrossAmount"] == DBNull.Value ? 0.00 : dr["GrossAmount"]);
    //            objRBPD.TDS = Convert.ToDecimal(dr["TDS"] == DBNull.Value ? 0.00 : dr["TDS"]);
    //            objRBPD.OtherDeduction = Convert.ToDecimal(dr["OtherDeduction"] == DBNull.Value ? 0.00 : dr["OtherDeduction"]);
    //            objRBPD.PayableAmount = Convert.ToDecimal(dr["PayableAmount"] == DBNull.Value ? 0.00 : dr["PayableAmount"]);
    //            objRBPD.RentBillAmount = Convert.ToDecimal(dr["RentBillAmount"] == DBNull.Value ? 0.00 : dr["RentBillAmount"]);
    //            objRBPD.CropYear = Convert.ToString(dr["CropYear"] == DBNull.Value ? String.Empty : dr["CropYear"]);
    //            objRBPD.FinancialYear = Convert.ToString(dr["FinancialYear"] == DBNull.Value ? String.Empty : dr["FinancialYear"]);
    //            objRBPD.Month = Convert.ToString(dr["Month"] == DBNull.Value ? String.Empty : dr["Month"]);
    //            objRBPD.Commodity = Convert.ToString(dr["Commodity"] == DBNull.Value ? String.Empty : dr["Commodity"]);
    //            listRentBillPaymentDetails.Add(objRBPD);
    //        }
    //    //JavaScriptSerializer js = new JavaScriptSerializer();
    //    //XmlSerializer js = new XmlSerializer(listRentBillPaymentDetails.ToString()); 
    //    //js.MaxJsonLength = Int32.MaxValue;
    //    //Context.Response.Write();
    //    //XmlSerializer tXmlSerializer = new XmlSerializer(ds.GetType());
    //    //System.IO.MemoryStream tStream = new System.IO.MemoryStream();
    //    //ds.DataSetName = "RentBillDetails";
    //    ////ds.Tables[0].TableName = "RentBillPaymentDetails";
    //    //ds.WriteXml(tStream);
    //    ////String tSerilaizedObject = System.Text.Encoding.UTF8.GetString(tStream.ToArray());
    //    //PaymentDatainXML = System.Text.Encoding.UTF8.GetString(tStream.ToArray());
    //    //return PaymentDatainXML;

    //    JavaScriptSerializer js = new JavaScriptSerializer();
    //    js.MaxJsonLength = Int32.MaxValue;
    //    Context.Response.Write(js.Serialize(listRentBillPaymentDetails));
    //}


    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true)]
    //[WebMethod(EnableSession = true)]
    public string GetRentBillDetails()
    {
        string PaymentRentBillNumber = HttpContext.Current.Request.QueryString["RentBillNumber"];
        //string PaymentDatainXML;
        List<RentBillPaymentDetails> listRentBillPaymentDetails = new List<RentBillPaymentDetails>();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlConnection con = new SqlConnection(str);
        con.Open();
        //string qry = "SELECT GRA.Ref_Bill_No as [StorageBillNumber], GRA.Bill_No as [RentBillNumber], SPB.Gross_Amount as [GrossAmount],SPB.TDS_Amt as [TDS]" +
        //"," + "SPB.OtherDeduction as [OtherDeduction], SPB.Payable_Amount as [PayableAmount], GRA.Net_Bill_Amount as [RentBillAmount], SPB.Crop_Year as [CropYear], SPB.Financial_Year as [FinancialYear]" +
        //"," + "SPB.Month as [Month], SPB.Commodity_Name as [Commodity] FROM StoragePayment_BillsFromCSMStoMPWLC_AfterAugust2020 SPB INNER JOIN " +
        //"" + " tbl_Godown_Rent_Deduction_Amount GRA ON SPB.Bill_Number = GRA.Ref_Bill_No WHERE GRA.Bill_No ='" + PaymentRentBillNumber + "'";
        string qry = "SELECT DSBRPO.Ref_Bill_No as [StorageBillNumber], DSBRPO.Bill_Number as [RentBillNumber],SPBCR.Gross_Amount as [GrossAmount], SPBCR.TDS_Amt as [TDS] " +
        "," + " SPBCR.OtherDeduction as [OtherDeduction], SPBCR.Payable_Amount as [ReceivedAmountFromMPSCSC], DSBRPO.Net_Amount as [RentBillAmountPaidToGodown], SPBCR.Crop_Year as [CropYear] " +
        "," + " SPBCR.Financial_Year as [FinancialYear], SPBCR.Month as [Month], SPBCR.Commodity_Name as [Commodity] " +
        "" + " FROM tbl_Digitally_Signed_Bill_RPO as DSBRPO INNER JOIN tbl_MetaData_GODOWN_2018 as MG " +
        "" + " ON MG.Godown_ID = DSBRPO.Godown_Id INNER JOIN tbl_Beneficiary_Account_Details as BAD " +
        "" + " ON BAD.Account_No = DSBRPO.Account_No INNER JOIN tbl_GdwnRentBill_Detuction_RM as GRBDRM " +
        "" + " ON GRBDRM.JVS_Bill_Number = DSBRPO.Ref_Bill_No INNER JOIN mpscsc.dbo.StoragePayment_BillsFromCSMStoMPWLC_AfterAugust2020 as SPBCR " +
        "" + " ON SPBCR.Bill_Number = GRBDRM.Ref_Bill_Number WHERE DSBRPO.Ref_Bill_No ='" + PaymentRentBillNumber + "'"; 
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        //da.Fill(dt);
        //ds.Tables.Add(dt);
        //ds.GetXml();
        //PaymentDatainXML = ds.GetXml().ToString();
        cmd.CommandTimeout = 0;
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {

            RentBillPaymentDetails objRBPD = new RentBillPaymentDetails();
            objRBPD.StorageBillNumber = Convert.ToString(dr["StorageBillNumber"] == DBNull.Value ? String.Empty : dr["StorageBillNumber"]);
            objRBPD.RentBillNumber = Convert.ToString(dr["RentBillNumber"] == DBNull.Value ? String.Empty : dr["RentBillNumber"]);
            objRBPD.GrossAmount = Convert.ToDecimal(dr["GrossAmount"] == DBNull.Value ? 0.00 : dr["GrossAmount"]);
            objRBPD.TDS = Convert.ToDecimal(dr["TDS"] == DBNull.Value ? 0.00 : dr["TDS"]);
            objRBPD.OtherDeduction = Convert.ToDecimal(dr["OtherDeduction"] == DBNull.Value ? 0.00 : dr["OtherDeduction"]);
            //objRBPD.PayableAmount = Convert.ToDecimal(dr["PayableAmount"] == DBNull.Value ? 0.00 : dr["PayableAmount"]);
            objRBPD.ReceivedAmountFromMPSCSC = Convert.ToDecimal(dr["ReceivedAmountFromMPSCSC"] == DBNull.Value ? 0.00 : dr["ReceivedAmountFromMPSCSC"]);
            //objRBPD.RentBillAmount = Convert.ToDecimal(dr["RentBillAmount"] == DBNull.Value ? 0.00 : dr["RentBillAmount"]);
            objRBPD.RentBillAmountPaidToGodown = Convert.ToDecimal(dr["RentBillAmountPaidToGodown"] == DBNull.Value ? 0.00 : dr["RentBillAmountPaidToGodown"]);
            objRBPD.CropYear = Convert.ToString(dr["CropYear"] == DBNull.Value ? String.Empty : dr["CropYear"]);
            objRBPD.FinancialYear = Convert.ToString(dr["FinancialYear"] == DBNull.Value ? String.Empty : dr["FinancialYear"]);
            objRBPD.Month = Convert.ToString(dr["Month"] == DBNull.Value ? String.Empty : dr["Month"]);
            objRBPD.Commodity = Convert.ToString(dr["Commodity"] == DBNull.Value ? String.Empty : dr["Commodity"]);
            listRentBillPaymentDetails.Add(objRBPD);
        }
        //JavaScriptSerializer js = new JavaScriptSerializer();
        //XmlSerializer js = new XmlSerializer(listRentBillPaymentDetails.ToString()); 
        //js.MaxJsonLength = Int32.MaxValue;
        //Context.Response.Write();
        //XmlSerializer tXmlSerializer = new XmlSerializer(ds.GetType());
        //System.IO.MemoryStream tStream = new System.IO.MemoryStream();
        //ds.DataSetName = "RentBillDetails";
        ////ds.Tables[0].TableName = "RentBillPaymentDetails";
        //ds.WriteXml(tStream);
        ////String tSerilaizedObject = System.Text.Encoding.UTF8.GetString(tStream.ToArray());
        //PaymentDatainXML = System.Text.Encoding.UTF8.GetString(tStream.ToArray());
        //return PaymentDatainXML;

        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = Int32.MaxValue;
        //Context.Response.Write(js.Serialize(listRentBillPaymentDetails));
        //return listRentBillPaymentDetails.ToList();
        return (js.Serialize(listRentBillPaymentDetails));


    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true)]
    public string GetBeneficiaryAccountDetails()
    {
        //string BeneficiaryAccountNumber = HttpContext.Current.Request.QueryString["BeneficiaryAccountNumber"];
        string BeneficiaryAccountNumber = HttpContext.Current.Request.QueryString["BAN"];
        List<BeneficiaryAccountDetails> listBenfAccountDetails = new List<BeneficiaryAccountDetails>();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlConnection con = new SqlConnection(str);
        con.Open();
        string qry = "SELECT distinct DT.Region_ID [RegionID], DT.Regionnm [RegionName], GA.Account_No [GodownOwnerAccountDetails], BAD.Beneficiary_Name [BeneficiaryName], BAD.IFSC_Code [IFSCCode]," +
        " " + "BAD.Mobile[BeneficiaryMobile], BAD.Email[BeneficiaryEmail], BAD.RO_Approval_Status, BAD.GO_Approval_Status" +
        " " + "FROM tbl_Godown_Owner_Account_Details GA inner join tbl_Beneficiary_Account_Details BAD on GA.Account_No = BAD.Account_No" +
        " " + "inner join tbl_MetaData_DEPOT as D on D.BranchId = GA.Branch_Id" +
        " " + "inner join tbl_MetaData_DISTRICT as DT on DT.District_Id = D.DistrictId" +
        " " + "inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID = GA.Godown_Id" +
        " " + "WHERE DT.Region_Id = '1' AND (BAD.Email LIKE '[A-Za-z0-9+_.-]%@%.%' OR BAD.Email is null OR BAD.Email='') AND GA.Account_No ='" + BeneficiaryAccountNumber +
        "'" + "AND BAD.RO_Approval_Status IN ('','Y') AND BAD.GO_Approval_Status IN ('Y','')";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        cmd.CommandTimeout = 0;
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {

            BeneficiaryAccountDetails objBAD = new BeneficiaryAccountDetails();
            objBAD.RegionID = Convert.ToString(dr["RegionID"] == DBNull.Value ? String.Empty : dr["RegionID"]);
            objBAD.RegionName = Convert.ToString(dr["RegionName"] == DBNull.Value ? String.Empty : dr["RegionName"]);
            objBAD.GodownOwnerAccountDetails = Convert.ToString(dr["GodownOwnerAccountDetails"] == DBNull.Value ? String.Empty : dr["GodownOwnerAccountDetails"]);
            objBAD.BeneficiaryName = Convert.ToString(dr["BeneficiaryName"] == DBNull.Value ? String.Empty : dr["BeneficiaryName"]);
            objBAD.IFSCCode = Convert.ToString(dr["IFSCCode"] == DBNull.Value ? String.Empty : dr["IFSCCode"]);
            objBAD.BeneficiaryMobile = Convert.ToString(dr["BeneficiaryMobile"] == DBNull.Value ? String.Empty : dr["BeneficiaryMobile"]);
            objBAD.BeneficiaryEmail = Convert.ToString(dr["BeneficiaryEmail"] == DBNull.Value ? String.Empty : dr["BeneficiaryEmail"]);
            listBenfAccountDetails.Add(objBAD);

        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = Int32.MaxValue;
        return (js.Serialize(listBenfAccountDetails));


    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true)]
    public string GetAllBeneficiaryAccountMasterData()
    {
        //string BeneficiaryAccountNumber = HttpContext.Current.Request.QueryString["BeneficiaryAccountNumber"];
        //string BeneficiaryAccountNumber = HttpContext.Current.Request.QueryString["BAN"];
        List<BeneficiaryAccountDetails> listBenfAccountDetails = new List<BeneficiaryAccountDetails>();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlConnection con = new SqlConnection(str);
        con.Open();
        string qry = "SELECT DISTINCT DT.Region_ID [RegionID], DT.Regionnm [RegionName], GA.Account_No [GodownOwnerAccountDetails], BAD.Beneficiary_Name [BeneficiaryName], BAD.IFSC_Code [IFSCCode]," +
        " " + "BAD.Mobile[BeneficiaryMobile], BAD.Email[BeneficiaryEmail], BAD.RO_Approval_Status, BAD.GO_Approval_Status" +
        " " + "FROM tbl_Godown_Owner_Account_Details GA inner join tbl_Beneficiary_Account_Details BAD on GA.Account_No = BAD.Account_No" +
        " " + "inner join tbl_MetaData_DEPOT as D on D.BranchId = GA.Branch_Id" +
        " " + "inner join tbl_MetaData_DISTRICT as DT on DT.District_Id = D.DistrictId" +
        " " + "inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID = GA.Godown_Id" +
        " " + "WHERE DT.Region_Id = '1' AND (BAD.Email LIKE '[A-Za-z0-9+_.-]%@%.%' OR BAD.Email is null OR BAD.Email='')" +
        " " + "AND BAD.RO_Approval_Status IN ('','Y') AND BAD.GO_Approval_Status IN ('Y','')";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        cmd.CommandTimeout = 0;
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {

            BeneficiaryAccountDetails objBAD = new BeneficiaryAccountDetails();
            objBAD.RegionID = Convert.ToString(dr["RegionID"] == DBNull.Value ? String.Empty : dr["RegionID"]);
            objBAD.RegionName = Convert.ToString(dr["RegionName"] == DBNull.Value ? String.Empty : dr["RegionName"]);
            objBAD.GodownOwnerAccountDetails = Convert.ToString(dr["GodownOwnerAccountDetails"] == DBNull.Value ? String.Empty : dr["GodownOwnerAccountDetails"]);
            objBAD.BeneficiaryName = Convert.ToString(dr["BeneficiaryName"] == DBNull.Value ? String.Empty : dr["BeneficiaryName"]);
            objBAD.IFSCCode = Convert.ToString(dr["IFSCCode"] == DBNull.Value ? String.Empty : dr["IFSCCode"]);
            objBAD.BeneficiaryMobile = Convert.ToString(dr["BeneficiaryMobile"] == DBNull.Value ? String.Empty : dr["BeneficiaryMobile"]);
            objBAD.BeneficiaryEmail = Convert.ToString(dr["BeneficiaryEmail"] == DBNull.Value ? String.Empty : dr["BeneficiaryEmail"]);
            listBenfAccountDetails.Add(objBAD);

        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = Int32.MaxValue;
        return (js.Serialize(listBenfAccountDetails));


    }

    /***********Jabalpur******************/
    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true)]
    public string GetBeneficiaryAccountDetailsForJabalpur()
    {
        //string BeneficiaryAccountNumber = HttpContext.Current.Request.QueryString["BeneficiaryAccountNumber"];
        string BeneficiaryAccountNumber = HttpContext.Current.Request.QueryString["BAN"];
        List<BeneficiaryAccountDetails> listBenfAccountDetails = new List<BeneficiaryAccountDetails>();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlConnection con = new SqlConnection(str);
        con.Open();
        string qry = "SELECT distinct DT.Region_ID [RegionID], DT.Regionnm [RegionName], GA.Account_No [GodownOwnerAccountDetails], BAD.Beneficiary_Name [BeneficiaryName], BAD.IFSC_Code [IFSCCode]," +
        " " + "BAD.Mobile[BeneficiaryMobile], BAD.Email[BeneficiaryEmail], BAD.RO_Approval_Status, BAD.GO_Approval_Status" +
        " " + "FROM tbl_Godown_Owner_Account_Details GA inner join tbl_Beneficiary_Account_Details BAD on GA.Account_No = BAD.Account_No" +
        " " + "inner join tbl_MetaData_DEPOT as D on D.BranchId = GA.Branch_Id" +
        " " + "inner join tbl_MetaData_DISTRICT as DT on DT.District_Id = D.DistrictId" +
        " " + "inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID = GA.Godown_Id" +
        " " + "WHERE DT.Region_Id = '3' AND (BAD.Email LIKE '[A-Za-z0-9+_.-]%@%.%' OR BAD.Email is null OR BAD.Email='') AND GA.Account_No ='" + BeneficiaryAccountNumber +
        "'" + "AND BAD.RO_Approval_Status IN ('','Y') AND BAD.GO_Approval_Status IN ('Y','')";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        cmd.CommandTimeout = 0;
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {

            BeneficiaryAccountDetails objBAD = new BeneficiaryAccountDetails();
            objBAD.RegionID = Convert.ToString(dr["RegionID"] == DBNull.Value ? String.Empty : dr["RegionID"]);
            objBAD.RegionName = Convert.ToString(dr["RegionName"] == DBNull.Value ? String.Empty : dr["RegionName"]);
            objBAD.GodownOwnerAccountDetails = Convert.ToString(dr["GodownOwnerAccountDetails"] == DBNull.Value ? String.Empty : dr["GodownOwnerAccountDetails"]);
            objBAD.BeneficiaryName = Convert.ToString(dr["BeneficiaryName"] == DBNull.Value ? String.Empty : dr["BeneficiaryName"]);
            objBAD.IFSCCode = Convert.ToString(dr["IFSCCode"] == DBNull.Value ? String.Empty : dr["IFSCCode"]);
            objBAD.BeneficiaryMobile = Convert.ToString(dr["BeneficiaryMobile"] == DBNull.Value ? String.Empty : dr["BeneficiaryMobile"]);
            objBAD.BeneficiaryEmail = Convert.ToString(dr["BeneficiaryEmail"] == DBNull.Value ? String.Empty : dr["BeneficiaryEmail"]);
            listBenfAccountDetails.Add(objBAD);

        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = Int32.MaxValue;
        return (js.Serialize(listBenfAccountDetails));


    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true)]
    public string GetAllBeneficiaryAccountMasterDataForJabalpur()
    {
        //string BeneficiaryAccountNumber = HttpContext.Current.Request.QueryString["BeneficiaryAccountNumber"];
        //string BeneficiaryAccountNumber = HttpContext.Current.Request.QueryString["BAN"];
        List<BeneficiaryAccountDetails> listBenfAccountDetails = new List<BeneficiaryAccountDetails>();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlConnection con = new SqlConnection(str);
        con.Open();
        string qry = "SELECT DISTINCT DT.Region_ID [RegionID], DT.Regionnm [RegionName], GA.Account_No [GodownOwnerAccountDetails], BAD.Beneficiary_Name [BeneficiaryName], BAD.IFSC_Code [IFSCCode]," +
        " " + "BAD.Mobile[BeneficiaryMobile], BAD.Email[BeneficiaryEmail], BAD.RO_Approval_Status, BAD.GO_Approval_Status" +
        " " + "FROM tbl_Godown_Owner_Account_Details GA inner join tbl_Beneficiary_Account_Details BAD on GA.Account_No = BAD.Account_No" +
        " " + "inner join tbl_MetaData_DEPOT as D on D.BranchId = GA.Branch_Id" +
        " " + "inner join tbl_MetaData_DISTRICT as DT on DT.District_Id = D.DistrictId" +
        " " + "inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID = GA.Godown_Id" +
        " " + "WHERE DT.Region_Id = '3' AND (BAD.Email LIKE '[A-Za-z0-9+_.-]%@%.%' OR BAD.Email is null OR BAD.Email='')" +
        " " + "AND BAD.RO_Approval_Status IN ('','Y') AND BAD.GO_Approval_Status IN ('Y','')";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        cmd.CommandTimeout = 0;
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {

            BeneficiaryAccountDetails objBAD = new BeneficiaryAccountDetails();
            objBAD.RegionID = Convert.ToString(dr["RegionID"] == DBNull.Value ? String.Empty : dr["RegionID"]);
            objBAD.RegionName = Convert.ToString(dr["RegionName"] == DBNull.Value ? String.Empty : dr["RegionName"]);
            objBAD.GodownOwnerAccountDetails = Convert.ToString(dr["GodownOwnerAccountDetails"] == DBNull.Value ? String.Empty : dr["GodownOwnerAccountDetails"]);
            objBAD.BeneficiaryName = Convert.ToString(dr["BeneficiaryName"] == DBNull.Value ? String.Empty : dr["BeneficiaryName"]);
            objBAD.IFSCCode = Convert.ToString(dr["IFSCCode"] == DBNull.Value ? String.Empty : dr["IFSCCode"]);
            objBAD.BeneficiaryMobile = Convert.ToString(dr["BeneficiaryMobile"] == DBNull.Value ? String.Empty : dr["BeneficiaryMobile"]);
            objBAD.BeneficiaryEmail = Convert.ToString(dr["BeneficiaryEmail"] == DBNull.Value ? String.Empty : dr["BeneficiaryEmail"]);
            listBenfAccountDetails.Add(objBAD);

        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = Int32.MaxValue;
        return (js.Serialize(listBenfAccountDetails));


    }

    /**********For Ujjain***************/
    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true)]
    public string GetBeneficiaryAccountDetailsForUjjain()
    {
        //string BeneficiaryAccountNumber = HttpContext.Current.Request.QueryString["BeneficiaryAccountNumber"];
        string BeneficiaryAccountNumber = HttpContext.Current.Request.QueryString["BAN"];
        List<BeneficiaryAccountDetails> listBenfAccountDetails = new List<BeneficiaryAccountDetails>();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlConnection con = new SqlConnection(str);
        con.Open();
        string qry = "SELECT distinct DT.Region_ID [RegionID], DT.Regionnm [RegionName], GA.Account_No [GodownOwnerAccountDetails], BAD.Beneficiary_Name [BeneficiaryName], BAD.IFSC_Code [IFSCCode]," +
        " " + "BAD.Mobile[BeneficiaryMobile], BAD.Email[BeneficiaryEmail], BAD.RO_Approval_Status, BAD.GO_Approval_Status" +
        " " + "FROM tbl_Godown_Owner_Account_Details GA inner join tbl_Beneficiary_Account_Details BAD on GA.Account_No = BAD.Account_No" +
        " " + "inner join tbl_MetaData_DEPOT as D on D.BranchId = GA.Branch_Id" +
        " " + "inner join tbl_MetaData_DISTRICT as DT on DT.District_Id = D.DistrictId" +
        " " + "inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID = GA.Godown_Id" +
        " " + "WHERE DT.Region_Id = '5' AND (BAD.Email LIKE '[A-Za-z0-9+_.-]%@%.%' OR BAD.Email is null OR BAD.Email='') AND GA.Account_No ='" + BeneficiaryAccountNumber +
        "'" + "AND BAD.RO_Approval_Status IN ('','Y') AND BAD.GO_Approval_Status IN ('Y','')";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        cmd.CommandTimeout = 0;
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {

            BeneficiaryAccountDetails objBAD = new BeneficiaryAccountDetails();
            objBAD.RegionID = Convert.ToString(dr["RegionID"] == DBNull.Value ? String.Empty : dr["RegionID"]);
            objBAD.RegionName = Convert.ToString(dr["RegionName"] == DBNull.Value ? String.Empty : dr["RegionName"]);
            objBAD.GodownOwnerAccountDetails = Convert.ToString(dr["GodownOwnerAccountDetails"] == DBNull.Value ? String.Empty : dr["GodownOwnerAccountDetails"]);
            objBAD.BeneficiaryName = Convert.ToString(dr["BeneficiaryName"] == DBNull.Value ? String.Empty : dr["BeneficiaryName"]);
            objBAD.IFSCCode = Convert.ToString(dr["IFSCCode"] == DBNull.Value ? String.Empty : dr["IFSCCode"]);
            objBAD.BeneficiaryMobile = Convert.ToString(dr["BeneficiaryMobile"] == DBNull.Value ? String.Empty : dr["BeneficiaryMobile"]);
            objBAD.BeneficiaryEmail = Convert.ToString(dr["BeneficiaryEmail"] == DBNull.Value ? String.Empty : dr["BeneficiaryEmail"]);
            listBenfAccountDetails.Add(objBAD);

        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = Int32.MaxValue;
        return (js.Serialize(listBenfAccountDetails));


    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true)]
    public string GetAllBeneficiaryAccountMasterDataForUjjain()
    {
        //string BeneficiaryAccountNumber = HttpContext.Current.Request.QueryString["BeneficiaryAccountNumber"];
        //string BeneficiaryAccountNumber = HttpContext.Current.Request.QueryString["BAN"];
        List<BeneficiaryAccountDetails> listBenfAccountDetails = new List<BeneficiaryAccountDetails>();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlConnection con = new SqlConnection(str);
        con.Open();
        string qry = "SELECT DISTINCT DT.Region_ID [RegionID], DT.Regionnm [RegionName], GA.Account_No [GodownOwnerAccountDetails], BAD.Beneficiary_Name [BeneficiaryName], BAD.IFSC_Code [IFSCCode]," +
        " " + "BAD.Mobile[BeneficiaryMobile], BAD.Email[BeneficiaryEmail], BAD.RO_Approval_Status, BAD.GO_Approval_Status" +
        " " + "FROM tbl_Godown_Owner_Account_Details GA inner join tbl_Beneficiary_Account_Details BAD on GA.Account_No = BAD.Account_No" +
        " " + "inner join tbl_MetaData_DEPOT as D on D.BranchId = GA.Branch_Id" +
        " " + "inner join tbl_MetaData_DISTRICT as DT on DT.District_Id = D.DistrictId" +
        " " + "inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID = GA.Godown_Id" +
        " " + "WHERE DT.Region_Id = '5' AND (BAD.Email LIKE '[A-Za-z0-9+_.-]%@%.%' OR BAD.Email is null OR BAD.Email='')" +
        " " + "AND BAD.RO_Approval_Status IN ('','Y') AND BAD.GO_Approval_Status IN ('Y','')";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        cmd.CommandTimeout = 0;
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {

            BeneficiaryAccountDetails objBAD = new BeneficiaryAccountDetails();
            objBAD.RegionID = Convert.ToString(dr["RegionID"] == DBNull.Value ? String.Empty : dr["RegionID"]);
            objBAD.RegionName = Convert.ToString(dr["RegionName"] == DBNull.Value ? String.Empty : dr["RegionName"]);
            objBAD.GodownOwnerAccountDetails = Convert.ToString(dr["GodownOwnerAccountDetails"] == DBNull.Value ? String.Empty : dr["GodownOwnerAccountDetails"]);
            objBAD.BeneficiaryName = Convert.ToString(dr["BeneficiaryName"] == DBNull.Value ? String.Empty : dr["BeneficiaryName"]);
            objBAD.IFSCCode = Convert.ToString(dr["IFSCCode"] == DBNull.Value ? String.Empty : dr["IFSCCode"]);
            objBAD.BeneficiaryMobile = Convert.ToString(dr["BeneficiaryMobile"] == DBNull.Value ? String.Empty : dr["BeneficiaryMobile"]);
            objBAD.BeneficiaryEmail = Convert.ToString(dr["BeneficiaryEmail"] == DBNull.Value ? String.Empty : dr["BeneficiaryEmail"]);
            listBenfAccountDetails.Add(objBAD);

        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = Int32.MaxValue;
        return (js.Serialize(listBenfAccountDetails));


    }


    /**********For Rewa***************/
    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true)]
    public string GetBeneficiaryAccountDetailsForRewa()
    {
        //string BeneficiaryAccountNumber = HttpContext.Current.Request.QueryString["BeneficiaryAccountNumber"];
        string BeneficiaryAccountNumber = HttpContext.Current.Request.QueryString["BAN"];
        List<BeneficiaryAccountDetails> listBenfAccountDetails = new List<BeneficiaryAccountDetails>();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlConnection con = new SqlConnection(str);
        con.Open();
        string qry = "SELECT distinct DT.Region_ID [RegionID], DT.Regionnm [RegionName], GA.Account_No [GodownOwnerAccountDetails], BAD.Beneficiary_Name [BeneficiaryName], BAD.IFSC_Code [IFSCCode]," +
        " " + "BAD.Mobile[BeneficiaryMobile], BAD.Email[BeneficiaryEmail], BAD.RO_Approval_Status, BAD.GO_Approval_Status" +
        " " + "FROM tbl_Godown_Owner_Account_Details GA inner join tbl_Beneficiary_Account_Details BAD on GA.Account_No = BAD.Account_No" +
        " " + "inner join tbl_MetaData_DEPOT as D on D.BranchId = GA.Branch_Id" +
        " " + "inner join tbl_MetaData_DISTRICT as DT on DT.District_Id = D.DistrictId" +
        " " + "inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID = GA.Godown_Id" +
        " " + "WHERE DT.Region_Id = '7' AND (BAD.Email LIKE '[A-Za-z0-9+_.-]%@%.%' OR BAD.Email is null OR BAD.Email='') AND GA.Account_No ='" + BeneficiaryAccountNumber +
        "'" + "AND BAD.RO_Approval_Status IN ('','Y') AND BAD.GO_Approval_Status IN ('Y','')";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        cmd.CommandTimeout = 0;
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {

            BeneficiaryAccountDetails objBAD = new BeneficiaryAccountDetails();
            objBAD.RegionID = Convert.ToString(dr["RegionID"] == DBNull.Value ? String.Empty : dr["RegionID"]);
            objBAD.RegionName = Convert.ToString(dr["RegionName"] == DBNull.Value ? String.Empty : dr["RegionName"]);
            objBAD.GodownOwnerAccountDetails = Convert.ToString(dr["GodownOwnerAccountDetails"] == DBNull.Value ? String.Empty : dr["GodownOwnerAccountDetails"]);
            objBAD.BeneficiaryName = Convert.ToString(dr["BeneficiaryName"] == DBNull.Value ? String.Empty : dr["BeneficiaryName"]);
            objBAD.IFSCCode = Convert.ToString(dr["IFSCCode"] == DBNull.Value ? String.Empty : dr["IFSCCode"]);
            objBAD.BeneficiaryMobile = Convert.ToString(dr["BeneficiaryMobile"] == DBNull.Value ? String.Empty : dr["BeneficiaryMobile"]);
            objBAD.BeneficiaryEmail = Convert.ToString(dr["BeneficiaryEmail"] == DBNull.Value ? String.Empty : dr["BeneficiaryEmail"]);
            listBenfAccountDetails.Add(objBAD);

        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = Int32.MaxValue;
        return (js.Serialize(listBenfAccountDetails));


    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true)]
    public string GetAllBeneficiaryAccountMasterDataForRewa()
    {
        //string BeneficiaryAccountNumber = HttpContext.Current.Request.QueryString["BeneficiaryAccountNumber"];
        //string BeneficiaryAccountNumber = HttpContext.Current.Request.QueryString["BAN"];
        List<BeneficiaryAccountDetails> listBenfAccountDetails = new List<BeneficiaryAccountDetails>();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlConnection con = new SqlConnection(str);
        con.Open();
        string qry = "SELECT DISTINCT DT.Region_ID [RegionID], DT.Regionnm [RegionName], GA.Account_No [GodownOwnerAccountDetails], BAD.Beneficiary_Name [BeneficiaryName], BAD.IFSC_Code [IFSCCode]," +
        " " + "BAD.Mobile[BeneficiaryMobile], BAD.Email[BeneficiaryEmail], BAD.RO_Approval_Status, BAD.GO_Approval_Status" +
        " " + "FROM tbl_Godown_Owner_Account_Details GA inner join tbl_Beneficiary_Account_Details BAD on GA.Account_No = BAD.Account_No" +
        " " + "inner join tbl_MetaData_DEPOT as D on D.BranchId = GA.Branch_Id" +
        " " + "inner join tbl_MetaData_DISTRICT as DT on DT.District_Id = D.DistrictId" +
        " " + "inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID = GA.Godown_Id" +
        " " + "WHERE DT.Region_Id = '7' AND (BAD.Email LIKE '[A-Za-z0-9+_.-]%@%.%' OR BAD.Email is null OR BAD.Email='')" +
        " " + "AND BAD.RO_Approval_Status IN ('','Y') AND BAD.GO_Approval_Status IN ('Y','')";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        cmd.CommandTimeout = 0;
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {

            BeneficiaryAccountDetails objBAD = new BeneficiaryAccountDetails();
            objBAD.RegionID = Convert.ToString(dr["RegionID"] == DBNull.Value ? String.Empty : dr["RegionID"]);
            objBAD.RegionName = Convert.ToString(dr["RegionName"] == DBNull.Value ? String.Empty : dr["RegionName"]);
            objBAD.GodownOwnerAccountDetails = Convert.ToString(dr["GodownOwnerAccountDetails"] == DBNull.Value ? String.Empty : dr["GodownOwnerAccountDetails"]);
            objBAD.BeneficiaryName = Convert.ToString(dr["BeneficiaryName"] == DBNull.Value ? String.Empty : dr["BeneficiaryName"]);
            objBAD.IFSCCode = Convert.ToString(dr["IFSCCode"] == DBNull.Value ? String.Empty : dr["IFSCCode"]);
            objBAD.BeneficiaryMobile = Convert.ToString(dr["BeneficiaryMobile"] == DBNull.Value ? String.Empty : dr["BeneficiaryMobile"]);
            objBAD.BeneficiaryEmail = Convert.ToString(dr["BeneficiaryEmail"] == DBNull.Value ? String.Empty : dr["BeneficiaryEmail"]);
            listBenfAccountDetails.Add(objBAD);

        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = Int32.MaxValue;
        return (js.Serialize(listBenfAccountDetails));


    }

    /**********For NARMADAPURAM***************/
    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true)]
    public string GetBeneficiaryAccountDetailsForNARMADAPURAM()
    {
        //string BeneficiaryAccountNumber = HttpContext.Current.Request.QueryString["BeneficiaryAccountNumber"];
        string BeneficiaryAccountNumber = HttpContext.Current.Request.QueryString["BAN"];
        List<BeneficiaryAccountDetails> listBenfAccountDetails = new List<BeneficiaryAccountDetails>();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlConnection con = new SqlConnection(str);
        con.Open();
        string qry = "SELECT distinct DT.Region_ID [RegionID], DT.Regionnm [RegionName], GA.Account_No [GodownOwnerAccountDetails], BAD.Beneficiary_Name [BeneficiaryName], BAD.IFSC_Code [IFSCCode]," +
        " " + "BAD.Mobile[BeneficiaryMobile], BAD.Email[BeneficiaryEmail], BAD.RO_Approval_Status, BAD.GO_Approval_Status" +
        " " + "FROM tbl_Godown_Owner_Account_Details GA inner join tbl_Beneficiary_Account_Details BAD on GA.Account_No = BAD.Account_No" +
        " " + "inner join tbl_MetaData_DEPOT as D on D.BranchId = GA.Branch_Id" +
        " " + "inner join tbl_MetaData_DISTRICT as DT on DT.District_Id = D.DistrictId" +
        " " + "inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID = GA.Godown_Id" +
        " " + "WHERE DT.Region_Id = '8' AND (BAD.Email LIKE '[A-Za-z0-9+_.-]%@%.%' OR BAD.Email is null OR BAD.Email='') AND GA.Account_No ='" + BeneficiaryAccountNumber +
        "'" + "AND BAD.RO_Approval_Status IN ('','Y') AND BAD.GO_Approval_Status IN ('Y','')";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        cmd.CommandTimeout = 0;
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {

            BeneficiaryAccountDetails objBAD = new BeneficiaryAccountDetails();
            objBAD.RegionID = Convert.ToString(dr["RegionID"] == DBNull.Value ? String.Empty : dr["RegionID"]);
            objBAD.RegionName = Convert.ToString(dr["RegionName"] == DBNull.Value ? String.Empty : dr["RegionName"]);
            objBAD.GodownOwnerAccountDetails = Convert.ToString(dr["GodownOwnerAccountDetails"] == DBNull.Value ? String.Empty : dr["GodownOwnerAccountDetails"]);
            objBAD.BeneficiaryName = Convert.ToString(dr["BeneficiaryName"] == DBNull.Value ? String.Empty : dr["BeneficiaryName"]);
            objBAD.IFSCCode = Convert.ToString(dr["IFSCCode"] == DBNull.Value ? String.Empty : dr["IFSCCode"]);
            objBAD.BeneficiaryMobile = Convert.ToString(dr["BeneficiaryMobile"] == DBNull.Value ? String.Empty : dr["BeneficiaryMobile"]);
            objBAD.BeneficiaryEmail = Convert.ToString(dr["BeneficiaryEmail"] == DBNull.Value ? String.Empty : dr["BeneficiaryEmail"]);
            listBenfAccountDetails.Add(objBAD);

        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = Int32.MaxValue;
        return (js.Serialize(listBenfAccountDetails));


    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true)]
    public string GetAllBeneficiaryAccountMasterDataForNARMADAPURAM()
    {
        //string BeneficiaryAccountNumber = HttpContext.Current.Request.QueryString["BeneficiaryAccountNumber"];
        //string BeneficiaryAccountNumber = HttpContext.Current.Request.QueryString["BAN"];
        List<BeneficiaryAccountDetails> listBenfAccountDetails = new List<BeneficiaryAccountDetails>();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlConnection con = new SqlConnection(str);
        con.Open();
        string qry = "SELECT DISTINCT DT.Region_ID [RegionID], DT.Regionnm [RegionName], GA.Account_No [GodownOwnerAccountDetails], BAD.Beneficiary_Name [BeneficiaryName], BAD.IFSC_Code [IFSCCode]," +
        " " + "BAD.Mobile[BeneficiaryMobile], BAD.Email[BeneficiaryEmail], BAD.RO_Approval_Status, BAD.GO_Approval_Status" +
        " " + "FROM tbl_Godown_Owner_Account_Details GA inner join tbl_Beneficiary_Account_Details BAD on GA.Account_No = BAD.Account_No" +
        " " + "inner join tbl_MetaData_DEPOT as D on D.BranchId = GA.Branch_Id" +
        " " + "inner join tbl_MetaData_DISTRICT as DT on DT.District_Id = D.DistrictId" +
        " " + "inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID = GA.Godown_Id" +
        " " + "WHERE DT.Region_Id = '8' AND (BAD.Email LIKE '[A-Za-z0-9+_.-]%@%.%' OR BAD.Email is null OR BAD.Email='')" +
        " " + "AND BAD.RO_Approval_Status IN ('','Y') AND BAD.GO_Approval_Status IN ('Y','')";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        cmd.CommandTimeout = 0;
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {

            BeneficiaryAccountDetails objBAD = new BeneficiaryAccountDetails();
            objBAD.RegionID = Convert.ToString(dr["RegionID"] == DBNull.Value ? String.Empty : dr["RegionID"]);
            objBAD.RegionName = Convert.ToString(dr["RegionName"] == DBNull.Value ? String.Empty : dr["RegionName"]);
            objBAD.GodownOwnerAccountDetails = Convert.ToString(dr["GodownOwnerAccountDetails"] == DBNull.Value ? String.Empty : dr["GodownOwnerAccountDetails"]);
            objBAD.BeneficiaryName = Convert.ToString(dr["BeneficiaryName"] == DBNull.Value ? String.Empty : dr["BeneficiaryName"]);
            objBAD.IFSCCode = Convert.ToString(dr["IFSCCode"] == DBNull.Value ? String.Empty : dr["IFSCCode"]);
            objBAD.BeneficiaryMobile = Convert.ToString(dr["BeneficiaryMobile"] == DBNull.Value ? String.Empty : dr["BeneficiaryMobile"]);
            objBAD.BeneficiaryEmail = Convert.ToString(dr["BeneficiaryEmail"] == DBNull.Value ? String.Empty : dr["BeneficiaryEmail"]);
            listBenfAccountDetails.Add(objBAD);

        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = Int32.MaxValue;
        return (js.Serialize(listBenfAccountDetails));


    }

    /**********************Get Region Details******************************/
    public class RegionDetails
    {
        public string RegionID;
        public string RegionName;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true)]
    public string GetAllRegionData()
    {
        List<RegionDetails> listRegionDetails = new List<RegionDetails>();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlConnection con = new SqlConnection(str);
        con.Open();
        string qry = "SELECT Region_ID [RegionID], Region [RegionName] FROM tbl_MetaData_Region";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        cmd.CommandTimeout = 0;
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {

            RegionDetails objRegion = new RegionDetails();
            objRegion.RegionID = Convert.ToString(dr["RegionID"] == DBNull.Value ? String.Empty : dr["RegionID"]);
            objRegion.RegionName = Convert.ToString(dr["RegionName"] == DBNull.Value ? String.Empty : dr["RegionName"]);
            listRegionDetails.Add(objRegion);

        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = Int32.MaxValue;
        return (js.Serialize(listRegionDetails));


    }


    /**********************Get Godown Details******************************/
    public class GodownDetails
    {
        public string GodownID;
        public string GodownName;
        public string GodownType;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true)]
    public string GetAllGodownData()
    {
        List<GodownDetails> listGodownDetails = new List<GodownDetails>();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlConnection con = new SqlConnection(str);
        con.Open();
        string qry = "select Godown_ID [GodownID],Godown_Name [GodownName],Hired_Type [GodownType] from tbl_MetaData_GODOWN_2018 where IsActive='Y'";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        cmd.CommandTimeout = 0;
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {

            GodownDetails objGodowndetails = new GodownDetails();
            objGodowndetails.GodownID = Convert.ToString(dr["GodownID"] == DBNull.Value ? String.Empty : dr["GodownID"]);
            objGodowndetails.GodownName = Convert.ToString(dr["GodownName"] == DBNull.Value ? String.Empty : dr["GodownName"]);
            objGodowndetails.GodownType = Convert.ToString(dr["GodownType"] == DBNull.Value ? String.Empty : dr["GodownType"]);
            listGodownDetails.Add(objGodowndetails);

        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = Int32.MaxValue;
        return (js.Serialize(listGodownDetails));


    }
    /**********************Get Refrence Number For Vacant Capacity Payment******************************/

    public class GetRefrenceNumberForVacantCapacity
    {
        public string Account_No;
        public string IFSC_Code;
        public decimal Credit_Amount;
        public string Reference_No;
        public string Bill_No;
        public string Godown_Id;

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true)]
    //[WebMethod(EnableSession = true)]
    public string GetVacantCapacityPaymentDetails()
    {
        //string ReferenceNo = HttpContext.Current.Request.QueryString["Reference_No"];
        //string PaymentDatainXML;
        List<GetRefrenceNumberForVacantCapacity> listGetRefrenceNumberForVacantCapacity = new List<GetRefrenceNumberForVacantCapacity>();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlConnection con = new SqlConnection(str);
        con.Open();
        //string qry = "SELECT GRA.Ref_Bill_No as [StorageBillNumber], GRA.Bill_No as [RentBillNumber], SPB.Gross_Amount as [GrossAmount],SPB.TDS_Amt as [TDS]" +
        //"," + "SPB.OtherDeduction as [OtherDeduction], SPB.Payable_Amount as [PayableAmount], GRA.Net_Bill_Amount as [RentBillAmount], SPB.Crop_Year as [CropYear], SPB.Financial_Year as [FinancialYear]" +
        //"," + "SPB.Month as [Month], SPB.Commodity_Name as [Commodity] FROM StoragePayment_BillsFromCSMStoMPWLC_AfterAugust2020 SPB INNER JOIN " +
        //"" + " tbl_Godown_Rent_Deduction_Amount GRA ON SPB.Bill_Number = GRA.Ref_Bill_No WHERE GRA.Bill_No ='" + PaymentRentBillNumber + "'";
        //string qry = "select Account_No,IFSC_Code,Credit_Amount,Reference_No,Bill_No,Godown_Id from tbl_Payment_Credit_Vaccant_Capacity WHERE Reference_No ='" + ReferenceNo + "'";
        string qry = "select Account_No,IFSC_Code,Credit_Amount,Reference_No,Bill_No,Godown_Id from tbl_Payment_Credit_Vaccant_Capacity";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        //da.Fill(dt);
        //ds.Tables.Add(dt);
        //ds.GetXml();
        //PaymentDatainXML = ds.GetXml().ToString();
        cmd.CommandTimeout = 0;
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {

            GetRefrenceNumberForVacantCapacity objRNVC = new GetRefrenceNumberForVacantCapacity();
            objRNVC.Account_No = Convert.ToString(dr["Account_No"] == DBNull.Value ? String.Empty : dr["Account_No"]);
            objRNVC.IFSC_Code = Convert.ToString(dr["IFSC_Code"] == DBNull.Value ? String.Empty : dr["IFSC_Code"]);
            objRNVC.Credit_Amount = Convert.ToDecimal(dr["Credit_Amount"] == DBNull.Value ? 0.00 : dr["Credit_Amount"]);
            objRNVC.Reference_No = Convert.ToString(dr["Reference_No"] == DBNull.Value ? String.Empty : dr["Reference_No"]);
            objRNVC.Bill_No = Convert.ToString(dr["Bill_No"] == DBNull.Value ? String.Empty : dr["Bill_No"]);
            objRNVC.Godown_Id = Convert.ToString(dr["Godown_Id"] == DBNull.Value ? String.Empty : dr["Godown_Id"]);
            listGetRefrenceNumberForVacantCapacity.Add(objRNVC);
        }
        //JavaScriptSerializer js = new JavaScriptSerializer();
        //XmlSerializer js = new XmlSerializer(listRentBillPaymentDetails.ToString()); 
        //js.MaxJsonLength = Int32.MaxValue;
        //Context.Response.Write();
        //XmlSerializer tXmlSerializer = new XmlSerializer(ds.GetType());
        //System.IO.MemoryStream tStream = new System.IO.MemoryStream();
        //ds.DataSetName = "RentBillDetails";
        ////ds.Tables[0].TableName = "RentBillPaymentDetails";
        //ds.WriteXml(tStream);
        ////String tSerilaizedObject = System.Text.Encoding.UTF8.GetString(tStream.ToArray());
        //PaymentDatainXML = System.Text.Encoding.UTF8.GetString(tStream.ToArray());
        //return PaymentDatainXML;

        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = Int32.MaxValue;
        //Context.Response.Write(js.Serialize(listRentBillPaymentDetails));
        //return listRentBillPaymentDetails.ToList();
        return (js.Serialize(listGetRefrenceNumberForVacantCapacity));


    }
}
