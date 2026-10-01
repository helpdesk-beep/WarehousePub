using System;
using System.Activities.Expressions;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Globalization;
using System.Drawing;

/// <summary>
/// Summary description for MPSCSC_InstituitionStorageBillDetails
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class MPSCSC_InstituitionStorageBillDetails : System.Web.Services.WebService
{

    public MPSCSC_InstituitionStorageBillDetails()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    //protected string getDate_MDY(string inDate)
    //{
    //    if (inDate == "" || inDate == null)
    //    {
    //        return "01/01/1919";
    //    }
    //    else
    //    {
    //        //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
    //        string converted = "";
    //        string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
    //        //converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
    //        //converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
    //        DateTime dt = DateTime.ParseExact(inDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    //        converted = dt.ToString("yyyy-MM-dd");
    //        return converted;
    //    }
    //}
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    //For Adding record in tbl_Institution_Storage_Bill_Details_Test (Test Table) 
    [WebMethod]
    public void EDAddInstitutionStorageBillDetails(string Bill_Number, string District_Id, string Branch_Id, string Depositor_Type_Id, string Depositor_Id,
        string Commodity_Type_Id, string Commodity_Id, string From_Date, string To_Date, string Packing_Type, string Weight, string Financial_Year, decimal Commodity_Rate,
        decimal Net_Amount, decimal Sub_Amount, decimal Service_Tax_Perc, decimal Service_Tax_Amt, string Depositor_Category, decimal Rebate_Perc, decimal Rebate_Amt, int Rebate_on_Unit,
        string Khasra_Number, string Rin_Pustika_No, string Cast_Certificate_No, string Is_Rebate, string Created_Date, string Modified_Date, string Client_IP, decimal Per_Day_Rate,
        string Bill_Type, int BId, int Month, string Godown_Id, string Crop_Year,
        decimal MPWLC_SC, decimal GST_Perc_SC, decimal GST_Amt_SC)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "INSERT INTO tbl_Institution_Storage_Bill_Details_Test(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId,Crop_Year,Month,Godown_Id,MPWLC_SC,GST_Perc_SC,GST_Amt_SC) " +
        //    "values('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Type_Id + "','" + Depositor_Id + "','" + Commodity_Type_Id + "','" + Commodity_Id + "','" + From_Date +
        //    "','" + To_Date + "','" + Packing_Type + "','" + Weight + "','" + Financial_Year + "','" + Commodity_Rate +
        //    "','" + Net_Amount + "','" + Sub_Amount + "','" + Service_Tax_Perc + "','" + Service_Tax_Amt +
        //    "','" + Depositor_Category + "','" + Rebate_Perc + "','" + Rebate_Amt + "','" + Rebate_on_Unit + "','" + Khasra_Number + "','" + Is_Rebate +
        //    "','" + Created_Date + "','" + Modified_Date + "','" + Client_IP + "','" + Per_Day_Rate + "','" + Rin_Pustika_No + "','" + Cast_Certificate_No +
        //    "','" + Bill_Type + "','" + BId + "','" + Crop_Year + "','" + Month + "','" + Godown_Id +
        //    "','" + MPWLC_SC + "','" + GST_Perc_SC + "','" + GST_Amt_SC + "')";
        //SqlCommand cmd = new SqlCommand(qry, con);
        const string qry = "INSERT INTO tbl_Institution_Storage_Bill_Details(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId,Crop_Year,Month,Godown_Id,MPWLC_SC,GST_Perc_SC,GST_Amt_SC) VALUES (@Bill_Number,@District_Id,@Branch_Id,@Depositor_Type_Id,@Depositor_Id,@Commodity_Type_Id,@Commodity_Id,@From_Date,@To_Date,@Packing_Type,@Weight,@Financial_Year,@Commodity_Rate,@Net_Amount,@Sub_Amount,@Service_Tax_Perc,@Service_Tax_Amt,@Depositor_Category,@Rebate_Perc,@Rebate_Amt,@Rebate_on_Unit,@Khasra_Number,@Is_Rebate,@Created_Date,@Modified_Date,@Client_IP,@Per_Day_Rate,@Rin_Pustika_No,@Cast_Certificate_No,@Bill_Type,@BId,@Crop_Year,@Month,@Godown_Id,@MPWLC_SC,@GST_Perc_SC,@GST_Amt_SC)";
        using (SqlCommand cmd = new SqlCommand(qry, con))
        {
            AddParameter(cmd, "@Bill_Number", Bill_Number);
            AddParameter(cmd, "@District_Id", District_Id);
            AddParameter(cmd, "@Branch_Id", Branch_Id);
            AddParameter(cmd, "@Depositor_Type_Id", Depositor_Type_Id);
            AddParameter(cmd, "@Depositor_Id", Depositor_Id);
            AddParameter(cmd, "@Commodity_Type_Id", Commodity_Type_Id);
            AddParameter(cmd, "@Commodity_Id", Commodity_Id);
            AddParameter(cmd, "@From_Date", From_Date);
            AddParameter(cmd, "@To_Date", To_Date);
            AddParameter(cmd, "@Packing_Type", Packing_Type);
            AddParameter(cmd, "@Weight", Weight);
            AddParameter(cmd, "@Financial_Year", Financial_Year);
            AddParameter(cmd, "@Commodity_Rate", Commodity_Rate);
            AddParameter(cmd, "@Net_Amount", Net_Amount);
            AddParameter(cmd, "@Sub_Amount", Sub_Amount);
            AddParameter(cmd, "@Service_Tax_Perc", Service_Tax_Perc);
            AddParameter(cmd, "@Service_Tax_Amt", Service_Tax_Amt);
            AddParameter(cmd, "@Depositor_Category", Depositor_Category);
            AddParameter(cmd, "@Rebate_Perc", Rebate_Perc);
            AddParameter(cmd, "@Rebate_Amt", Rebate_Amt);
            AddParameter(cmd, "@Rebate_on_Unit", Rebate_on_Unit);
            AddParameter(cmd, "@Khasra_Number", Khasra_Number);
            AddParameter(cmd, "@Is_Rebate", Is_Rebate);
            AddParameter(cmd, "@Created_Date", Created_Date);
            AddParameter(cmd, "@Modified_Date", Modified_Date);
            AddParameter(cmd, "@Client_IP", Client_IP);
            AddParameter(cmd, "@Per_Day_Rate", Per_Day_Rate);
            AddParameter(cmd, "@Rin_Pustika_No", Rin_Pustika_No);
            AddParameter(cmd, "@Cast_Certificate_No", Cast_Certificate_No);
            AddParameter(cmd, "@Bill_Type", Bill_Type);
            AddParameter(cmd, "@BId", BId);
            AddParameter(cmd, "@Crop_Year", Crop_Year);
            AddParameter(cmd, "@Month", Month);
            AddParameter(cmd, "@Godown_Id", Godown_Id);
            AddParameter(cmd, "@MPWLC_SC", MPWLC_SC);
            AddParameter(cmd, "@GST_Perc_SC", GST_Perc_SC);
            AddParameter(cmd, "@GST_Amt_SC", GST_Amt_SC);
            cmd.ExecuteNonQuery();
        }
        con.Close();
    }

    //For Adding record in tbl_Bill_Institution_Daily_Charges_Test (Test Table)
    [WebMethod]
    public void EDAddInstitutionStorageBillCharges(string Bill_Number, string Dates, decimal Opening_Balance, decimal Receive_Bags, decimal Issue_Bags, decimal Closing_Balance, decimal Per_Day_Rate,
        decimal Total_Charges, string Created_Date, string Modified_Date, decimal Opening_Weight, decimal Receive_Weight, decimal Issue_Weight, decimal Closing_Weight, string Client_IP,
        string Godown_Id, int Commodity_ID, string Crop_Year, string Bill_Type)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "INSERT INTO tbl_Bill_Institution_Daily_Charges_Test(Bill_Number,Dates,Opening_Balance,Receive_Bags,Issue_Bags,Closing_Balance,Per_Day_Rate,Total_Charges," +
        //  "Created_Date,Modified_Date,Opening_Weight,Receive_Weight,Issue_Weight,Closing_Weight,Client_Ip,Godown_Id,Commodity_ID,Crop_Year,Bill_Type)" +
        //  "VALUES('" + Bill_Number + "','" + Dates + "','" + Opening_Balance + "','" + Receive_Bags + "','" + Issue_Bags + "','" + Closing_Balance +
        //  "','" + Per_Day_Rate + "','" + Total_Charges + "','" + Created_Date + "','" + Modified_Date +
        //  "','" + Opening_Weight + "','" + Receive_Weight + "','" + Issue_Weight + "','" + Closing_Weight + "','" + Client_IP + "','" + Godown_Id + "','" + Commodity_ID +
        //  "','" + Crop_Year + "','" + Bill_Type + "')";
        //SqlCommand cmd = new SqlCommand(qry, con);
        string qry = "INSERT INTO tbl_Bill_Institution_Daily_Charges(Bill_Number,Dates,Opening_Balance,Receive_Bags,Issue_Bags,Closing_Balance,Per_Day_Rate,Total_Charges," +
          "Created_Date,Modified_Date,Opening_Weight,Receive_Weight,Issue_Weight,Closing_Weight,Client_Ip,Godown_Id,Commodity_ID,Crop_Year,Bill_Type)" +
          "VALUES('" + Bill_Number + "','" + Dates + "','" + Opening_Balance + "','" + Receive_Bags + "','" + Issue_Bags + "','" + Closing_Balance +
          "','" + Per_Day_Rate + "','" + Total_Charges + "','" + Created_Date + "','" + Modified_Date +
          "','" + Opening_Weight + "','" + Receive_Weight + "','" + Issue_Weight + "','" + Closing_Weight + "','" + Client_IP + "','" + Godown_Id + "','" + Commodity_ID +
          "','" + Crop_Year + "','" + Bill_Type + "')";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        con.Close();
    }

    //For Adding in tbl_Institution_Storage_Bill_Summary_Test (Test Table)
    [WebMethod]
    public void EDAddInstitutionStorageBillSummary(string Bill_Number, string District_Id, string Branch_Id, string Depositor_Type_Id, string Depositor_Id,
        string Commodity_Type_Id, string Commodity_Id, string From_Date, string To_Date, string Packing_Type,
        string Weight, string Financial_Year, decimal Commodity_Rate, decimal Net_Amount, decimal Sub_Amount,
        decimal Service_Tax_Perc, decimal Service_Tax_Amt, string Depositor_Category, decimal Rebate_Perc, decimal Rebate_Amt, int Rebate_on_Unit,
        string Khasra_Number, string Rin_Pustika_No, string Cast_Certificate_No, string Is_Rebate, string Created_Date, string Modified_Date, string Client_IP, decimal Per_Day_Rate,
        string Bill_Type, int BId, int Month, string Year, string Crop_Year, decimal MPWLC_SC, decimal GST_Perc_SC, decimal GST_Amt_SC, int Bill_Count)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "INSERT INTO tbl_Institution_Storage_Bill_Summary_Test(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type," +
        //   "Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number," +
        //   "Rin_Pustika_No,Cast_Certificate_No,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Bill_Type,BId,Month,Year,Crop_Year,MPWLC_SC,GST_Perc_SC,GST_Amt_SC,Bill_Count) " +
        //   "values('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Type_Id + "','" + Depositor_Id + "','" + Commodity_Type_Id + "','" + Commodity_Id + "','" + From_Date.ToString() +
        //   "','" + To_Date + "','" + Packing_Type + "','" + Weight + "','" + Financial_Year + "','" + Commodity_Rate +
        //   "','" + Net_Amount + "','" + Sub_Amount + "','" + Service_Tax_Perc + "','" + Service_Tax_Amt +
        //   "','" + Depositor_Category + "','" + Rebate_Perc + "','" + Rebate_Amt + "','" + Rebate_on_Unit + "','" + Khasra_Number + "','" + Rin_Pustika_No + "','" + Cast_Certificate_No +
        //   "','" + Is_Rebate + "','" + Created_Date + "','" + Modified_Date + "','" + Client_IP + "','" + Per_Day_Rate + "','" + Bill_Type + "','" + BId +
        //   "','" + Month + "','" + Year + "','" + Crop_Year + "','" + MPWLC_SC + "','" + GST_Perc_SC + "','" + GST_Amt_SC + "','" + Bill_Count + "')";
        //SqlCommand cmd = new SqlCommand(qry, con);
        const string qry = "INSERT INTO tbl_Institution_Storage_Bill_Summary(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Rin_Pustika_No,Cast_Certificate_No,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Bill_Type,BId,Month,Year,Crop_Year,MPWLC_SC,GST_Perc_SC,GST_Amt_SC,Bill_Count) VALUES (@Bill_Number,@District_Id,@Branch_Id,@Depositor_Type_Id,@Depositor_Id,@Commodity_Type_Id,@Commodity_Id,@From_Date,@To_Date,@Packing_Type,@Weight,@Financial_Year,@Commodity_Rate,@Net_Amount,@Sub_Amount,@Service_Tax_Perc,@Service_Tax_Amt,@Depositor_Category,@Rebate_Perc,@Rebate_Amt,@Rebate_on_Unit,@Khasra_Number,@Rin_Pustika_No,@Cast_Certificate_No,@Is_Rebate,@Created_Date,@Modified_Date,@Client_IP,@Per_Day_Rate,@Bill_Type,@BId,@Month,@Year,@Crop_Year,@MPWLC_SC,@GST_Perc_SC,@GST_Amt_SC,@Bill_Count)";
        using (SqlCommand cmd = new SqlCommand(qry, con))
        {
            AddParameter(cmd, "@Bill_Number", Bill_Number);
            AddParameter(cmd, "@District_Id", District_Id);
            AddParameter(cmd, "@Branch_Id", Branch_Id);
            AddParameter(cmd, "@Depositor_Type_Id", Depositor_Type_Id);
            AddParameter(cmd, "@Depositor_Id", Depositor_Id);
            AddParameter(cmd, "@Commodity_Type_Id", Commodity_Type_Id);
            AddParameter(cmd, "@Commodity_Id", Commodity_Id);
            AddParameter(cmd, "@From_Date", From_Date);
            AddParameter(cmd, "@To_Date", To_Date);
            AddParameter(cmd, "@Packing_Type", Packing_Type);
            AddParameter(cmd, "@Weight", Weight);
            AddParameter(cmd, "@Financial_Year", Financial_Year);
            AddParameter(cmd, "@Commodity_Rate", Commodity_Rate);
            AddParameter(cmd, "@Net_Amount", Net_Amount);
            AddParameter(cmd, "@Sub_Amount", Sub_Amount);
            AddParameter(cmd, "@Service_Tax_Perc", Service_Tax_Perc);
            AddParameter(cmd, "@Service_Tax_Amt", Service_Tax_Amt);
            AddParameter(cmd, "@Depositor_Category", Depositor_Category);
            AddParameter(cmd, "@Rebate_Perc", Rebate_Perc);
            AddParameter(cmd, "@Rebate_Amt", Rebate_Amt);
            AddParameter(cmd, "@Rebate_on_Unit", Rebate_on_Unit);
            AddParameter(cmd, "@Khasra_Number", Khasra_Number);
            AddParameter(cmd, "@Rin_Pustika_No", Rin_Pustika_No);
            AddParameter(cmd, "@Cast_Certificate_No", Cast_Certificate_No);
            AddParameter(cmd, "@Is_Rebate", Is_Rebate);
            AddParameter(cmd, "@Created_Date", Created_Date);
            AddParameter(cmd, "@Modified_Date", Modified_Date);
            AddParameter(cmd, "@Client_IP", Client_IP);
            AddParameter(cmd, "@Per_Day_Rate", Per_Day_Rate);
            AddParameter(cmd, "@Bill_Type", Bill_Type);
            AddParameter(cmd, "@BId", BId);
            AddParameter(cmd, "@Month", Month);
            AddParameter(cmd, "@Year", Year);
            AddParameter(cmd, "@Crop_Year", Crop_Year);
            AddParameter(cmd, "@MPWLC_SC", MPWLC_SC);
            AddParameter(cmd, "@GST_Perc_SC", GST_Perc_SC);
            AddParameter(cmd, "@GST_Amt_SC", GST_Amt_SC);
            AddParameter(cmd, "@Bill_Count", Bill_Count);
            cmd.ExecuteNonQuery();
        }
        con.Close();
    }

    //For Updating FinBillNo in  tbl_Institution_Storage_Bill_Details_Test (Test Table)
    [WebMethod]
    public void EDUpdateInstituitionStorageBillDetailswithFinBillNo(string Bill_No, string Godown_Bill_No, string Godown_ID)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "UPDATE tbl_Institution_Storage_Bill_Details_Test SET Fin_Bill_No='" + Bill_No + "' where Bill_Number='" + Godown_Bill_No + "' and Godown_Id='" + Godown_ID + "'";
        const string qry = "UPDATE tbl_Institution_Storage_Bill_Details SET Fin_Bill_No=@Bill_No where Bill_Number=@Godown_Bill_No and Godown_Id=@Godown_ID";
        using (SqlCommand cmd = new SqlCommand(qry, con))
        {
            AddParameter(cmd, "@Bill_No", Bill_No);
            AddParameter(cmd, "@Godown_Bill_No", Godown_Bill_No);
            AddParameter(cmd, "@Godown_ID", Godown_ID);
            cmd.ExecuteNonQuery();
        }
        con.Close();
    }

    //For Updating BO Approval Status and their related info in tbl_Institution_Storage_Bill_Summary_Test Summary (Test Table) 
    [WebMethod]
    public void EDUpdateBOApprovalInSummary(string Bill_Number, string District_Id, string Branch_ID, string Client_IP)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "UPDATE tbl_Institution_Storage_Bill_Summary_Test SET BO_Approval_Status='Y',BO_Approval_Date=GETDATE(),BO_Approval_IP='" + Client_IP + "'" +
        //"WHERE Bill_Number='" + Bill_Number + "' " +
        //"AND Branch_Id='" + Branch_ID + "' " +
        //"AND District_Id='" + District_Id + "'";
        //SqlCommand cmd = new SqlCommand(qry, con);
        const string qry = "UPDATE tbl_Institution_Storage_Bill_Summary SET BO_Approval_Status='Y',BO_Approval_Date=GETDATE(),BO_Approval_IP=@Client_IP WHERE Bill_Number=@Bill_Number AND Branch_Id=@Branch_ID AND District_Id=@District_Id";
        using (SqlCommand cmd = new SqlCommand(qry, con))
        {
            AddParameter(cmd, "@Client_IP", Client_IP);
            AddParameter(cmd, "@Bill_Number", Bill_Number);
            AddParameter(cmd, "@Branch_ID", Branch_ID);
            AddParameter(cmd, "@District_Id", District_Id);
            cmd.ExecuteNonQuery();
        }
        con.Close();
    }

    //For Updating BO Approval Status and their related info in  tbl_Institution_Storage_Bill_Details_Test Bill Details (Test Table) 
    [WebMethod]
    public void EDUpdateBOApprovalInDetails(string Bill_Number, string District_Id, string Branch_ID, string Client_IP)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "UPDATE tbl_Institution_Storage_Bill_Details_Test SET BO_Approval_Status='Y',BO_Approval_Date=GETDATE(),BO_Approval_IP='" + Client_IP + "'" +
        //"WHERE Fin_Bill_No='" + Bill_Number + "' " +
        //"AND Branch_Id='" + Branch_ID + "' " +
        //"AND District_Id='" + District_Id + "'";
        //SqlCommand cmd = new SqlCommand(qry, con);
        const string qry = "UPDATE tbl_Institution_Storage_Bill_Details SET BO_Approval_Status='Y',BO_Approval_Date=GETDATE(),BO_Approval_IP=@Client_IP WHERE Fin_Bill_No=@Bill_Number AND Branch_Id=@Branch_ID AND District_Id=@District_Id";
        using (SqlCommand cmd = new SqlCommand(qry, con))
        {
            AddParameter(cmd, "@Client_IP", Client_IP);
            AddParameter(cmd, "@Bill_Number", Bill_Number);
            AddParameter(cmd, "@Branch_ID", Branch_ID);
            AddParameter(cmd, "@District_Id", District_Id);
            cmd.ExecuteNonQuery();
        }
        con.Close();
    }

    //For Deleting Record from tbl_Institution_Storage_Bill_Details and inserting it in tbl_Institution_Storage_Bill_Details_Log table (Test Table) 
    [WebMethod]
    public void EDAddInstitutionStorageBillDetailsInLog(string Bill_Number, string Branch_Id, string Client_IP)

    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "INSERT INTO tbl_Institution_Storage_Bill_Details_Log_Test(Sno,Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight," +
        //    "Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt," +
        //    "Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId,Crop_Year,Month,Godown_Id,MPWLC_SC,GST_Perc_SC,GST_Amt_SC) " +
        //    "SELECT [Sno],[Bill_Number],[District_Id],[Branch_Id],[Depositor_Type_Id],[Depositor_Id],[Commodity_Type_Id], [Commodity_Id],[From_Date], [To_Date], [Packing_Type],[Weight], [Financial_Year],[Commodity_Rate]," +
        //    "[Net_Amount],[Sub_Amount], [Service_Tax_Perc],[Service_Tax_Amt], [Depositor_Category],[Rebate_Perc], [Rebate_Amt],[Rebate_on_Unit], " +
        //    "[Khasra_Number],[Rin_Pustika_No], [Cast_Certificate_No],[Is_Rebate]," +
        //    "[Created_Date],[Modified_Date],[Client_IP],[Per_Day_Rate],[Bill_Type],[BId],[Month],[Godown_Id],[Crop_Year],[UpdatedBy],[UpdatedDate]," +
        //    "'" + Client_IP + "', GETDATE() '" +
        //    "[BO_Approval_Status],[BO_Approval_Date],[BO_Approval_IP],[RO_Approval_Status],[RO_Approval_Date],[RO_Approval_IP],[HO_Approval_Status],[HO_Approval_Date],[HO_Approval_IP]," +
        //    "[MPWLC_SC],[GST_Perc_SC],[GST_Amt_SC],[fin_Bill_No],[Delete_Flag],[Year] FROM [dbo].[tbl_Institution_Storage_Bill_Details] WHERE Bill_Number='" + Bill_Number + "' AND " +
        //    "Branch_Id='" + Branch_Id + "' " +
        //    "AND Bill_Number not in (select CDS.Ref_Bill_No from MPSCSC.dbo.Digitally_Sign_StorageBill_IC as CDS where Branch_Id='" + Branch_Id + "' and CDS.Ref_Bill_No is not null)";

        //string qry = "insert into tbl_Institution_Storage_Bill_Details_Log_Test (Sno,[Bill_Number],[District_Id],[Branch_Id],[Depositor_Type_Id],[Depositor_Id],[Commodity_Type_Id],[Commodity_Id],[From_Date],[To_Date],[Packing_Type],[Weight],[Financial_Year],[Commodity_Rate],[Net_Amount],[Sub_Amount],[Service_Tax_Perc],[Service_Tax_Amt],[Depositor_Category],[Rebate_Perc],[Rebate_Amt],[Rebate_on_Unit],[Khasra_Number],[Rin_Pustika_No],[Cast_Certificate_No],[Is_Rebate],[Created_Date],[Modified_Date],[Client_IP],[Per_Day_Rate],[Bill_Type],[BId],[Month],[Godown_Id],[Crop_Year],[UpdatedBy],[UpdatedDate],DeletedBy,DeletedDate,[BO_Approval_Status],[BO_Approval_Date],[BO_Approval_IP],[RO_Approval_Status],[RO_Approval_Date],[RO_Approval_IP],[HO_Approval_Status],[HO_Approval_Date],[HO_Approval_IP],MPWLC_SC,GST_Perc_SC,GST_Amt_SC,Fin_Bill_No,Delete_Flag)" +
        //                                "select Sno,[Bill_Number],[District_Id],[Branch_Id],[Depositor_Type_Id],[Depositor_Id],[Commodity_Type_Id],[Commodity_Id],[From_Date],[To_Date],[Packing_Type],[Weight],[Financial_Year],[Commodity_Rate],[Net_Amount],[Sub_Amount],[Service_Tax_Perc],[Service_Tax_Amt],[Depositor_Category],[Rebate_Perc],[Rebate_Amt],[Rebate_on_Unit],[Khasra_Number],[Rin_Pustika_No],[Cast_Certificate_No],[Is_Rebate],[Created_Date],[Modified_Date],[Client_IP],[Per_Day_Rate],[Bill_Type],[BId],[Month],[Godown_Id],[Crop_Year],[UpdatedBy],[UpdatedDate],'" + Client_IP + "',getdate(),[BO_Approval_Status],[BO_Approval_Date],[BO_Approval_IP],[RO_Approval_Status],[RO_Approval_Date],[RO_Approval_IP],[HO_Approval_Status],[HO_Approval_Date],[HO_Approval_IP],MPWLC_SC,GST_Perc_SC,GST_Amt_SC,Fin_Bill_No,'R' from tbl_Institution_Storage_Bill_Details_Test where Bill_Number='" + Bill_Number + "' and Branch_Id='" + Branch_Id + "' and Bill_Number not in (select CDS.Ref_Bill_No " +
        //                                "from MPSCSC.dbo.Digitally_Sign_StorageBill_IC as CDS where Branch_Id='" + Branch_Id + "' and CDS.Ref_Bill_No is not null)";
        //SqlCommand cmd = new SqlCommand(qry, con);
        string qry = "insert into tbl_Institution_Storage_Bill_Details_Log (Sno,[Bill_Number],[District_Id],[Branch_Id],[Depositor_Type_Id],[Depositor_Id],[Commodity_Type_Id],[Commodity_Id],[From_Date],[To_Date],[Packing_Type],[Weight],[Financial_Year],[Commodity_Rate],[Net_Amount],[Sub_Amount],[Service_Tax_Perc],[Service_Tax_Amt],[Depositor_Category],[Rebate_Perc],[Rebate_Amt],[Rebate_on_Unit],[Khasra_Number],[Rin_Pustika_No],[Cast_Certificate_No],[Is_Rebate],[Created_Date],[Modified_Date],[Client_IP],[Per_Day_Rate],[Bill_Type],[BId],[Month],[Godown_Id],[Crop_Year],[UpdatedBy],[UpdatedDate],DeletedBy,DeletedDate,[BO_Approval_Status],[BO_Approval_Date],[BO_Approval_IP],[RO_Approval_Status],[RO_Approval_Date],[RO_Approval_IP],[HO_Approval_Status],[HO_Approval_Date],[HO_Approval_IP],MPWLC_SC,GST_Perc_SC,GST_Amt_SC,Fin_Bill_No,Delete_Flag)" +
                                        "select Sno,[Bill_Number],[District_Id],[Branch_Id],[Depositor_Type_Id],[Depositor_Id],[Commodity_Type_Id],[Commodity_Id],[From_Date],[To_Date],[Packing_Type],[Weight],[Financial_Year],[Commodity_Rate],[Net_Amount],[Sub_Amount],[Service_Tax_Perc],[Service_Tax_Amt],[Depositor_Category],[Rebate_Perc],[Rebate_Amt],[Rebate_on_Unit],[Khasra_Number],[Rin_Pustika_No],[Cast_Certificate_No],[Is_Rebate],[Created_Date],[Modified_Date],[Client_IP],[Per_Day_Rate],[Bill_Type],[BId],[Month],[Godown_Id],[Crop_Year],[UpdatedBy],[UpdatedDate],'" + Client_IP + "',getdate(),[BO_Approval_Status],[BO_Approval_Date],[BO_Approval_IP],[RO_Approval_Status],[RO_Approval_Date],[RO_Approval_IP],[HO_Approval_Status],[HO_Approval_Date],[HO_Approval_IP],MPWLC_SC,GST_Perc_SC,GST_Amt_SC,Fin_Bill_No,'R' from tbl_Institution_Storage_Bill_Details_Test where Bill_Number='" + Bill_Number + "' and Branch_Id='" + Branch_Id + "' and Bill_Number not in (select CDS.Ref_Bill_No " +
                                        "from MPSCSC.dbo.Digitally_Sign_StorageBill_IC as CDS where Branch_Id='" + Branch_Id + "' and CDS.Ref_Bill_No is not null)";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        con.Close();
    }

    [WebMethod]
    public void EDAddInstitutionStorageBillDetailsInLogDeletebyBillNo(string Bill_Number, string Client_IP)

    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "INSERT INTO tbl_Institution_Storage_Bill_Details_Log_Test(Sno,Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight," +
        //    "Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt," +
        //    "Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId,Crop_Year,Month,Godown_Id,MPWLC_SC,GST_Perc_SC,GST_Amt_SC) " +
        //    "SELECT [Sno],[Bill_Number],[District_Id],[Branch_Id],[Depositor_Type_Id],[Depositor_Id],[Commodity_Type_Id], [Commodity_Id],[From_Date], [To_Date], [Packing_Type],[Weight], [Financial_Year],[Commodity_Rate]," +
        //    "[Net_Amount],[Sub_Amount], [Service_Tax_Perc],[Service_Tax_Amt], [Depositor_Category],[Rebate_Perc], [Rebate_Amt],[Rebate_on_Unit], " +
        //    "[Khasra_Number],[Rin_Pustika_No], [Cast_Certificate_No],[Is_Rebate]," +
        //    "[Created_Date],[Modified_Date],[Client_IP],[Per_Day_Rate],[Bill_Type],[BId],[Month],[Godown_Id],[Crop_Year],[UpdatedBy],[UpdatedDate]," +
        //    "'" + Client_IP + "', GETDATE() '" +
        //    "[BO_Approval_Status],[BO_Approval_Date],[BO_Approval_IP],[RO_Approval_Status],[RO_Approval_Date],[RO_Approval_IP],[HO_Approval_Status],[HO_Approval_Date],[HO_Approval_IP]," +
        //    "[MPWLC_SC],[GST_Perc_SC],[GST_Amt_SC],[fin_Bill_No],[Delete_Flag],[Year] FROM [dbo].[tbl_Institution_Storage_Bill_Details] WHERE Bill_Number='" + Bill_Number + "' AND " +
        //    "Branch_Id='" + Branch_Id + "' " +
        //    "AND Bill_Number not in (select CDS.Ref_Bill_No from MPSCSC.dbo.Digitally_Sign_StorageBill_IC as CDS where Branch_Id='" + Branch_Id + "' and CDS.Ref_Bill_No is not null)";
        //string qry = "insert into tbl_Institution_Storage_Bill_Details_Log_Test (Sno,[Bill_Number],[District_Id],[Branch_Id],[Depositor_Type_Id],[Depositor_Id],[Commodity_Type_Id],[Commodity_Id],[From_Date],[To_Date],[Packing_Type],[Weight],[Financial_Year],[Commodity_Rate],[Net_Amount],[Sub_Amount],[Service_Tax_Perc],[Service_Tax_Amt],[Depositor_Category],[Rebate_Perc],[Rebate_Amt],[Rebate_on_Unit],[Khasra_Number],[Rin_Pustika_No],[Cast_Certificate_No],[Is_Rebate],[Created_Date],[Modified_Date],[Client_IP],[Per_Day_Rate],[Bill_Type],[BId],[Month],[Godown_Id],[Crop_Year],[UpdatedBy],[UpdatedDate],DeletedBy,DeletedDate,[BO_Approval_Status],[BO_Approval_Date],[BO_Approval_IP],[RO_Approval_Status],[RO_Approval_Date],[RO_Approval_IP],[HO_Approval_Status],[HO_Approval_Date],[HO_Approval_IP],MPWLC_SC,GST_Perc_SC,GST_Amt_SC,Fin_Bill_No,Delete_Flag)" +
        //                                    "select D.Sno,D.[Bill_Number],D.[District_Id],D.[Branch_Id],D.[Depositor_Type_Id],D.[Depositor_Id],D.[Commodity_Type_Id],D.[Commodity_Id],D.[From_Date],D.[To_Date],D.[Packing_Type],D.[Weight],D.[Financial_Year],D.[Commodity_Rate],D.[Net_Amount],D.[Sub_Amount],D.[Service_Tax_Perc],D.[Service_Tax_Amt],D.[Depositor_Category],D.[Rebate_Perc],D.[Rebate_Amt],D.[Rebate_on_Unit],D.[Khasra_Number],D.[Rin_Pustika_No],D.[Cast_Certificate_No],D.[Is_Rebate],D.[Created_Date],D.[Modified_Date],D.[Client_IP],D.[Per_Day_Rate],D.[Bill_Type],D.[BId],D.[Month],D.[Godown_Id],D.[Crop_Year],D.[UpdatedBy],D.[UpdatedDate],'" + Client_IP + "',getdate(),D.[BO_Approval_Status],D.[BO_Approval_Date],D.[BO_Approval_IP],D.[RO_Approval_Status],D.[RO_Approval_Date],D.[RO_Approval_IP],D.[HO_Approval_Status],D.[HO_Approval_Date],D.[HO_Approval_IP],D.MPWLC_SC,GST_Perc_SC,D.GST_Amt_SC,D.Fin_Bill_No,'R' from tbl_Institution_Storage_Bill_Details_Test D " +
        //                                    "LEFT JOIN MPSCSC.dbo.Digitally_Sign_StorageBill_IC IC ON IC.Ref_Bill_No=D.Fin_Bill_No " +
        //                                    "where D.Bill_Number='" + Bill_Number + "' and IC.Ref_Bill_No IS NULL";
        string qry = "insert into tbl_Institution_Storage_Bill_Details_Log (Sno,[Bill_Number],[District_Id],[Branch_Id],[Depositor_Type_Id],[Depositor_Id],[Commodity_Type_Id],[Commodity_Id],[From_Date],[To_Date],[Packing_Type],[Weight],[Financial_Year],[Commodity_Rate],[Net_Amount],[Sub_Amount],[Service_Tax_Perc],[Service_Tax_Amt],[Depositor_Category],[Rebate_Perc],[Rebate_Amt],[Rebate_on_Unit],[Khasra_Number],[Rin_Pustika_No],[Cast_Certificate_No],[Is_Rebate],[Created_Date],[Modified_Date],[Client_IP],[Per_Day_Rate],[Bill_Type],[BId],[Month],[Godown_Id],[Crop_Year],[UpdatedBy],[UpdatedDate],DeletedBy,DeletedDate,[BO_Approval_Status],[BO_Approval_Date],[BO_Approval_IP],[RO_Approval_Status],[RO_Approval_Date],[RO_Approval_IP],[HO_Approval_Status],[HO_Approval_Date],[HO_Approval_IP],MPWLC_SC,GST_Perc_SC,GST_Amt_SC,Fin_Bill_No,Delete_Flag)" +
                                            "select D.Sno,D.[Bill_Number],D.[District_Id],D.[Branch_Id],D.[Depositor_Type_Id],D.[Depositor_Id],D.[Commodity_Type_Id],D.[Commodity_Id],D.[From_Date],D.[To_Date],D.[Packing_Type],D.[Weight],D.[Financial_Year],D.[Commodity_Rate],D.[Net_Amount],D.[Sub_Amount],D.[Service_Tax_Perc],D.[Service_Tax_Amt],D.[Depositor_Category],D.[Rebate_Perc],D.[Rebate_Amt],D.[Rebate_on_Unit],D.[Khasra_Number],D.[Rin_Pustika_No],D.[Cast_Certificate_No],D.[Is_Rebate],D.[Created_Date],D.[Modified_Date],D.[Client_IP],D.[Per_Day_Rate],D.[Bill_Type],D.[BId],D.[Month],D.[Godown_Id],D.[Crop_Year],D.[UpdatedBy],D.[UpdatedDate],'" + Client_IP + "',getdate(),D.[BO_Approval_Status],D.[BO_Approval_Date],D.[BO_Approval_IP],D.[RO_Approval_Status],D.[RO_Approval_Date],D.[RO_Approval_IP],D.[HO_Approval_Status],D.[HO_Approval_Date],D.[HO_Approval_IP],D.MPWLC_SC,GST_Perc_SC,D.GST_Amt_SC,D.Fin_Bill_No,'R' from tbl_Institution_Storage_Bill_Details_Test D " +
                                            "LEFT JOIN MPSCSC.dbo.Digitally_Sign_StorageBill_IC IC ON IC.Ref_Bill_No=D.Fin_Bill_No " +
                                            "where D.Bill_Number='" + Bill_Number + "' and IC.Ref_Bill_No IS NULL";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        con.Close();
    }

    //For Deleting Record from tbl_Institution_Storage_Bill_Details_Test (Test Table) 
    [WebMethod]
    public void EDDeleteInstitutionStorageBillDetails(string Bill_Number, string Branch_Id)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "DELETE FROM tbl_Institution_Storage_Bill_Details_Test WHERE Bill_Number='" + Bill_Number + "'AND Branch_Id = '" + Branch_Id + "' AND Bill_Number NOT IN (SELECT CDS.Ref_Bill_No FROM MPSCSC.dbo.Digitally_Sign_StorageBill_IC as CDS WHERE Branch_Id = '" + Branch_Id + "' and CDS.Ref_Bill_No is not null)";
        const string qry = "DELETE FROM tbl_Institution_Storage_Bill_Details WHERE Bill_Number=@Bill_Number AND Branch_Id=@Branch_Id AND Bill_Number NOT IN (SELECT CDS.Ref_Bill_No FROM MPSCSC.dbo.Digitally_Sign_StorageBill_IC as CDS WHERE Branch_Id=@Branch_Id and CDS.Ref_Bill_No is not null)";
        using (SqlCommand cmd = new SqlCommand(qry, con))
        {
            AddParameter(cmd, "@Bill_Number", Bill_Number);
            AddParameter(cmd, "@Branch_Id", Branch_Id);
            cmd.ExecuteNonQuery();
        }
        con.Close();
    }

    private static void AddParameter(SqlCommand command, string name, object value)
    {
        command.Parameters.AddWithValue(name, value ?? DBNull.Value);
    }

    [WebMethod]
    public void EDDeleteInstitutionStorageBillDetailsbyBillNo(string Bill_Number)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "Delete BD from tbl_Institution_Storage_Bill_Details_Test BD LEFT JOIN MPSCSC.dbo.Digitally_Sign_StorageBill_IC as CDS ON BD.Bill_Number=CDS.Ref_Bill_No WHERE BD.Bill_Number='" + Bill_Number + "' AND CDS.Ref_Bill_No IS NULL";
        string qry = "Delete BD from tbl_Institution_Storage_Bill_Details BD LEFT JOIN MPSCSC.dbo.Digitally_Sign_StorageBill_IC as CDS ON BD.Bill_Number=CDS.Ref_Bill_No WHERE BD.Bill_Number='" + Bill_Number + "' AND CDS.Ref_Bill_No IS NULL";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        con.Close();
    }

    //For Adding Record in tbl_Bill_Institution_Daily_Charges_Log_Test (Test Table) 
    [WebMethod]
    public void EDAddInstitutionBillDailyChargesInLog(string Bill_Number,string Branch_Id, string Client_IP)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "INSERT INTO tbl_Bill_Institution_Daily_Charges_Log_Test SELECT [Id],[Bill_Number],[Dates],[Opening_Balance],[Receive_Bags],[Issue_Bags],[Closing_Balance],[Per_Day_Rate],[Total_Charges],[Created_Date],[Modified_Date],[Client_IP],[Godown_Id],[Opening_Weight],[Receive_Weight],[Issue_Weight],[Closing_Weight],'" + Client_IP + "',getdate() FROM [tbl_Bill_Institution_Daily_Charges_Test] where Bill_Number='" + Bill_Number + "'";
        string qry = "INSERT INTO tbl_Bill_Institution_Daily_Charges_Log SELECT [Id],[Bill_Number],[Dates],[Opening_Balance],[Receive_Bags],[Issue_Bags],[Closing_Balance],[Per_Day_Rate],[Total_Charges],[Created_Date],[Modified_Date],[Client_IP],[Godown_Id],[Opening_Weight],[Receive_Weight],[Issue_Weight],[Closing_Weight],'" + Client_IP + "',getdate() FROM [tbl_Bill_Institution_Daily_Charges_Test] where Bill_Number='" + Bill_Number + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        con.Close();
    }

    [WebMethod]
    public void EDAddInstitutionBillDailyChargesInLogByBillNo(string Bill_Number, string Client_IP)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
       // string qry = "insert into tbl_Bill_Institution_Daily_Charges_Log_Test(Id,[Bill_Number],[Dates],[Opening_Balance],[Receive_Bags],[Issue_Bags],[Closing_Balance],[Per_Day_Rate],[Total_Charges],[Created_Date],[Modified_Date],[Client_IP],[Godown_Id],[Opening_Weight],[Receive_Weight],[Issue_Weight],[Closing_Weight],DeletedBy,DeletedDate) SELECT Id,[Bill_Number],[Dates],[Opening_Balance],[Receive_Bags],[Issue_Bags],[Closing_Balance],[Per_Day_Rate],[Total_Charges],[Created_Date],[Modified_Date],[Client_IP],[Godown_Id],[Opening_Weight],[Receive_Weight],[Issue_Weight],[Closing_Weight],'" + Client_IP + "',getdate() FROM [tbl_Bill_Institution_Daily_Charges_Test] where Bill_Number='" + Bill_Number + "'";
        string qry = "insert into tbl_Bill_Institution_Daily_Charges_Log(Id,[Bill_Number],[Dates],[Opening_Balance],[Receive_Bags],[Issue_Bags],[Closing_Balance],[Per_Day_Rate],[Total_Charges],[Created_Date],[Modified_Date],[Client_IP],[Godown_Id],[Opening_Weight],[Receive_Weight],[Issue_Weight],[Closing_Weight],DeletedBy,DeletedDate) SELECT Id,[Bill_Number],[Dates],[Opening_Balance],[Receive_Bags],[Issue_Bags],[Closing_Balance],[Per_Day_Rate],[Total_Charges],[Created_Date],[Modified_Date],[Client_IP],[Godown_Id],[Opening_Weight],[Receive_Weight],[Issue_Weight],[Closing_Weight],'" + Client_IP + "',getdate() FROM [tbl_Bill_Institution_Daily_Charges_Test] where Bill_Number='" + Bill_Number + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        con.Close();
    }

    //For Deleting Record from tbl_Bill_Institution_Daily_Charges_Test (Test Table) 
    [WebMethod]
    public void EDDeleteInstitutionStorageDailyBillCharges(string Bill_Number)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "DELETE FROM tbl_Bill_Institution_Daily_Charges_Test WHERE Bill_Number='" + Bill_Number + "'";
        string qry = "DELETE FROM tbl_Bill_Institution_Daily_Charges WHERE Bill_Number='" + Bill_Number + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        con.Close();
    }

    //For Adding Record in tbl_Godown_Rent_Deduction_Amount_Log_Test (Test Table) 
    [WebMethod]
    public void EDAddGodownRentDeductionAmountInLog(string Bill_Number, string District_Id, string Branch_Id, string Client_IP)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "INSERT INTO tbl_Godown_Rent_Deduction_Amount_Log_Test SELECT [AID],[Bill_No],[District_Id],[Branch_Id],[Godown_Id],[Dunnage_PS_Amt],[Elect_Beam_Scale_Amt],[Wooden_Planke_Amt],[Ana_Kit_Set_Amt],[Fumigation_Cover_Amt],[Fire_Extinguisher_Amt],[Fire_Buckets_Amt],[Security_Guard_Amt],[Sprey_Pump_Amt],[Digital_Moisture_Meter_Amt],[Fumigation_Emloyee_Amt],[Common_Facility_Amt],[Insectiside_Cost],[TResources_Deduct_Amt],[TBill_Deduct_Amt],[TBill_Amount],[Net_Bill_Amount],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],'" + Client_IP + "',getdate(),[Godown_Closing_Balance],[Total_Stock_Deposit],[Ref_Bill_No] FROM[Intergrated_MP_STORAGE].[dbo].[tbl_Godown_Rent_Deduction_Amount_Test] where Ref_Bill_No = '" + Bill_Number + "' and Branch_Id = '" + Branch_Id + "'";
        string qry = "INSERT INTO tbl_Godown_Rent_Deduction_Amount_Log SELECT [AID],[Bill_No],[District_Id],[Branch_Id],[Godown_Id],[Dunnage_PS_Amt],[Elect_Beam_Scale_Amt],[Wooden_Planke_Amt],[Ana_Kit_Set_Amt],[Fumigation_Cover_Amt],[Fire_Extinguisher_Amt],[Fire_Buckets_Amt],[Security_Guard_Amt],[Sprey_Pump_Amt],[Digital_Moisture_Meter_Amt],[Fumigation_Emloyee_Amt],[Common_Facility_Amt],[Insectiside_Cost],[TResources_Deduct_Amt],[TBill_Deduct_Amt],[TBill_Amount],[Net_Bill_Amount],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],'" + Client_IP + "',getdate(),[Godown_Closing_Balance],[Total_Stock_Deposit],[Ref_Bill_No] FROM[Intergrated_MP_STORAGE].[dbo].[tbl_Godown_Rent_Deduction_Amount_Test] where Ref_Bill_No = '" + Bill_Number + "' and Branch_Id = '" + Branch_Id + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        con.Close();
    }

    //For Deleting Record from tbl_Godown_Rent_Deduction_Amount_Test (Test Table) 
    [WebMethod]
    public void EDDeleteGodownRentDeductionAmount(string Bill_Number, string Branch_Id)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "DELETE FROM tbl_Godown_Rent_Deduction_Amount_Test where Ref_Bill_No='" + Bill_Number + "' and Branch_Id='" + Branch_Id + "'";
        string qry = "DELETE FROM tbl_Godown_Rent_Deduction_Amount where Ref_Bill_No='" + Bill_Number + "' and Branch_Id='" + Branch_Id + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        con.Close();
    }

    [WebMethod]
    public void EDDeleteGodownRentDeductionAmountByBillNo(string Bill_Number)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "DELETE FROM tbl_Godown_Rent_Deduction_Amount_Test where Ref_Bill_No='" + Bill_Number + "'";
        string qry = "DELETE FROM tbl_Godown_Rent_Deduction_Amount where Ref_Bill_No='" + Bill_Number + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        con.Close();
    }

    //For Adding Record in tbl_Bill_PVT_Godown_Daily_Rent_Log_Test (Test Table) 
    [WebMethod]
    public void EDAddPrivateGodownDailyRentInLog(string Bill_Number, string Client_IP)

    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "INSERT INTO tbl_Bill_PVT_Godown_Daily_Rent_log_Test SELECT [Id],[Bill_Number],[Dates],[Opening_Weight],[Receive_Weight],[Issue_Weight],[Closing_Weight],[Opening_Balance],[Receive_Bags],[Issue_Bags]," +
        //"[Closing_Bag_Balance],[Per_Day_Rate],[Total_Charges],[Created_Date],[Modified_Date],'" + Client_IP + "',getdate() FROM [tbl_Bill_PVT_Godown_Daily_Rent_Test] WHERE Bill_Number='" + Bill_Number + "'";
        //string qry= "insert into tbl_Bill_PVT_Godown_Daily_Rent_log_Test SELECT Id,[Bill_Number],[Dates],[Opening_Weight],[Receive_Weight],[Issue_Weight],[Closing_Weight],[Opening_Balance],[Receive_Bags],[Issue_Bags],[Closing_Bag_Balance],[Per_Day_Rate],[Total_Charges],[Created_Date],[Modified_Date],'" + Client_IP + "',getdate() FROM [tbl_Bill_PVT_Godown_Daily_Rent_Test] where Bill_Number='" + Bill_Number + "'";
        string qry = "insert into tbl_Bill_PVT_Godown_Daily_Rent_log SELECT Id,[Bill_Number],[Dates],[Opening_Weight],[Receive_Weight],[Issue_Weight],[Closing_Weight],[Opening_Balance],[Receive_Bags],[Issue_Bags],[Closing_Bag_Balance],[Per_Day_Rate],[Total_Charges],[Created_Date],[Modified_Date],'" + Client_IP + "',getdate() FROM [tbl_Bill_PVT_Godown_Daily_Rent_Test] where Bill_Number='" + Bill_Number + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        con.Close();
    }

    //For Deleting Record from tbl_Bill_PVT_Godown_Daily_Rent_Test (Test Table) 
    [WebMethod]
    public void EDDeletePvtBillGodownDailyRentAmount(string Bill_Number)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "DELETE FROM tbl_Bill_PVT_Godown_Daily_Rent_Test WHERE Ref_Bill_No='" + Bill_Number + "'";
        string qry = "DELETE FROM tbl_Bill_PVT_Godown_Daily_Rent WHERE Ref_Bill_No='" + Bill_Number + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        con.Close();
    }

    //For Adding Record in tbl_Godown_Rent_Deduction_Amount_Log_Test (Test Table) 
    [WebMethod]
    public void EDAddGodownRentDeductionLog(string Bill_Number, string Branch_Id, string Client_IP)

    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "INSERT INTO tbl_Godown_Rent_Deduction_Amount_Log_Test SELECT[AID],[Bill_No],[District_Id],[Branch_Id],[Godown_Id],[Dunnage_PS_Amt],[Elect_Beam_Scale_Amt],[Wooden_Planke_Amt]," +
        //"[Ana_Kit_Set_Amt],[Fumigation_Cover_Amt],[Fire_Extinguisher_Amt],[Fire_Buckets_Amt],[Security_Guard_Amt],[Sprey_Pump_Amt],[Digital_Moisture_Meter_Amt],[Fumigation_Emloyee_Amt],[Common_Facility_Amt]," +
        //"[Insectiside_Cost],[TResources_Deduct_Amt],[TBill_Deduct_Amt],[TBill_Amount],[Net_Bill_Amount],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],'" + Client_IP + "',getdate()," +
        //"[Godown_Closing_Balance],[Total_Stock_Deposit],[Ref_Bill_No] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Godown_Rent_Deduction_Amount_Test] where Bill_No = '" + Bill_Number + "' and Branch_Id = '" + Branch_Id + "'";
        //string qry = "insert into tbl_Godown_Rent_Deduction_Amount_Log_Test SELECT [AID],[Bill_No],[District_Id],[Branch_Id],[Godown_Id],[Dunnage_PS_Amt],[Elect_Beam_Scale_Amt],[Wooden_Planke_Amt],[Ana_Kit_Set_Amt],[Fumigation_Cover_Amt],[Fire_Extinguisher_Amt],[Fire_Buckets_Amt],[Security_Guard_Amt],[Sprey_Pump_Amt],[Digital_Moisture_Meter_Amt],[Fumigation_Emloyee_Amt],[Common_Facility_Amt],[Insectiside_Cost],[TResources_Deduct_Amt],[TBill_Deduct_Amt],[TBill_Amount],[Net_Bill_Amount],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],'" + Client_IP + "',getdate(),[Godown_Closing_Balance],[Total_Stock_Deposit],[Ref_Bill_No] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Godown_Rent_Deduction_Amount_Test] where Ref_Bill_No='" + Bill_Number + "' and Branch_Id='" + Branch_Id + "'";
        string qry = "insert into tbl_Godown_Rent_Deduction_Amount_Log SELECT [AID],[Bill_No],[District_Id],[Branch_Id],[Godown_Id],[Dunnage_PS_Amt],[Elect_Beam_Scale_Amt],[Wooden_Planke_Amt],[Ana_Kit_Set_Amt],[Fumigation_Cover_Amt],[Fire_Extinguisher_Amt],[Fire_Buckets_Amt],[Security_Guard_Amt],[Sprey_Pump_Amt],[Digital_Moisture_Meter_Amt],[Fumigation_Emloyee_Amt],[Common_Facility_Amt],[Insectiside_Cost],[TResources_Deduct_Amt],[TBill_Deduct_Amt],[TBill_Amount],[Net_Bill_Amount],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],'" + Client_IP + "',getdate(),[Godown_Closing_Balance],[Total_Stock_Deposit],[Ref_Bill_No] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Godown_Rent_Deduction_Amount_Test] where Ref_Bill_No='" + Bill_Number + "' and Branch_Id='" + Branch_Id + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        con.Close();
    }

    [WebMethod]
    public void EDAddGodownRentDeductionLogByBillNo(string Bill_Number,string Client_IP)

    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "INSERT INTO tbl_Godown_Rent_Deduction_Amount_Log_Test SELECT[AID],[Bill_No],[District_Id],[Branch_Id],[Godown_Id],[Dunnage_PS_Amt],[Elect_Beam_Scale_Amt],[Wooden_Planke_Amt]," +
        //"[Ana_Kit_Set_Amt],[Fumigation_Cover_Amt],[Fire_Extinguisher_Amt],[Fire_Buckets_Amt],[Security_Guard_Amt],[Sprey_Pump_Amt],[Digital_Moisture_Meter_Amt],[Fumigation_Emloyee_Amt],[Common_Facility_Amt]," +
        //"[Insectiside_Cost],[TResources_Deduct_Amt],[TBill_Deduct_Amt],[TBill_Amount],[Net_Bill_Amount],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],'" + Client_IP + "',getdate()," +
        //"[Godown_Closing_Balance],[Total_Stock_Deposit],[Ref_Bill_No] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Godown_Rent_Deduction_Amount_Test] where Bill_No = '" + Bill_Number + "' and Branch_Id = '" + Branch_Id + "'";
        //string qry = "insert into tbl_Godown_Rent_Deduction_Amount_Log_Test ([AID],[Bill_No],[District_Id],[Branch_Id],[Godown_Id],[Dunnage_PS_Amt],[Elect_Beam_Scale_Amt],[Wooden_Planke_Amt],[Ana_Kit_Set_Amt],[Fumigation_Cover_Amt],[Fire_Extinguisher_Amt],[Fire_Buckets_Amt],[Security_Guard_Amt],[Sprey_Pump_Amt],[Digital_Moisture_Meter_Amt],[Fumigation_Emloyee_Amt],[Common_Facility_Amt],[Insectiside_Cost],[TResources_Deduct_Amt],[TBill_Deduct_Amt],[TBill_Amount],[Net_Bill_Amount],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],DeletedBy,DeletedDate,[Godown_Closing_Balance],[Total_Stock_Deposit],[Ref_Bill_No]) SELECT [AID],[Bill_No],[District_Id],[Branch_Id],[Godown_Id],[Dunnage_PS_Amt],[Elect_Beam_Scale_Amt],[Wooden_Planke_Amt],[Ana_Kit_Set_Amt],[Fumigation_Cover_Amt],[Fire_Extinguisher_Amt],[Fire_Buckets_Amt],[Security_Guard_Amt],[Sprey_Pump_Amt],[Digital_Moisture_Meter_Amt],[Fumigation_Emloyee_Amt],[Common_Facility_Amt],[Insectiside_Cost],[TResources_Deduct_Amt],[TBill_Deduct_Amt],[TBill_Amount],[Net_Bill_Amount],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],'" + Client_IP + "',getdate(),[Godown_Closing_Balance],[Total_Stock_Deposit],[Ref_Bill_No] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Godown_Rent_Deduction_Amount_Test] where Ref_Bill_No='" + Bill_Number + "'";
        string qry = "insert into tbl_Godown_Rent_Deduction_Amount_Log ([AID],[Bill_No],[District_Id],[Branch_Id],[Godown_Id],[Dunnage_PS_Amt],[Elect_Beam_Scale_Amt],[Wooden_Planke_Amt],[Ana_Kit_Set_Amt],[Fumigation_Cover_Amt],[Fire_Extinguisher_Amt],[Fire_Buckets_Amt],[Security_Guard_Amt],[Sprey_Pump_Amt],[Digital_Moisture_Meter_Amt],[Fumigation_Emloyee_Amt],[Common_Facility_Amt],[Insectiside_Cost],[TResources_Deduct_Amt],[TBill_Deduct_Amt],[TBill_Amount],[Net_Bill_Amount],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],DeletedBy,DeletedDate,[Godown_Closing_Balance],[Total_Stock_Deposit],[Ref_Bill_No]) SELECT [AID],[Bill_No],[District_Id],[Branch_Id],[Godown_Id],[Dunnage_PS_Amt],[Elect_Beam_Scale_Amt],[Wooden_Planke_Amt],[Ana_Kit_Set_Amt],[Fumigation_Cover_Amt],[Fire_Extinguisher_Amt],[Fire_Buckets_Amt],[Security_Guard_Amt],[Sprey_Pump_Amt],[Digital_Moisture_Meter_Amt],[Fumigation_Emloyee_Amt],[Common_Facility_Amt],[Insectiside_Cost],[TResources_Deduct_Amt],[TBill_Deduct_Amt],[TBill_Amount],[Net_Bill_Amount],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],'" + Client_IP + "',getdate(),[Godown_Closing_Balance],[Total_Stock_Deposit],[Ref_Bill_No] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Godown_Rent_Deduction_Amount_Test] where Ref_Bill_No='" + Bill_Number + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        con.Close();
    }

    //For Deleting Final Bill Record from Summary table and Digital Sign (Test Table) 
    [WebMethod]
    public void EDDeleteFinalBill(string Bill_Number, string District_Id, string Branch_Id, string Client_IP)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        SqlCommand cmd = new SqlCommand("dbo.Delete_tbl_Institution_Storage_Bill_Summary_Test", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Bill_Number", Bill_Number);
        cmd.Parameters.AddWithValue("@District_Id", District_Id);
        cmd.Parameters.AddWithValue("@Branch_Id", Branch_Id);
        cmd.Parameters.AddWithValue("@IPAddress", Client_IP);
        cmd.Parameters.AddWithValue("@DeleteFlag", "R");
        cmd.ExecuteNonQuery();
        con.Close();
    }

    //For Updating Bill Approval Details in Institution Storage Bill Details tbl_Institution_Storage_Bill_Details_Test (Test Table)
    [WebMethod]
    public void EDUpdateBOApprovalInstitutionStorageBillDetails(string Bill_Number, string District_Id, string Branch_Id, string BO_Approval_Status, DateTime BO_Approval_Date, string BO_Approval_IP)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "UPDATE tbl_Institution_Storage_Bill_Details_Test SET BO_Approval_Status='Y',BO_Approval_Date=GETDATE(),BO_Approval_IP='" + BO_Approval_IP + "'" +
        //"WHERE Bill_Number='" + Bill_Number + "' and Branch_Id='" + Branch_Id + "' and District_Id='" + District_Id + "'";
        //SqlCommand cmd = new SqlCommand(qry, con);
        string qry = "UPDATE tbl_Institution_Storage_Bill_Details SET BO_Approval_Status='Y',BO_Approval_Date=GETDATE(),BO_Approval_IP='" + BO_Approval_IP + "'" +
       "WHERE Bill_Number='" + Bill_Number + "' and Branch_Id='" + Branch_Id + "' and District_Id='" + District_Id + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        con.Close();
    }

    //For Adding record in tbl_Institution_Storage_Bill_Details_Test (Test Table) 
    [WebMethod]
    public void EDAddInstitutionStorageBillDetailsForGodownRent(string Bill_Number, string District_Id, string Branch_Id, string Depositor_Type_Id, string Depositor_Id,
        string Commodity_Id, string From_Date, string To_Date, string Packing_Type, string Weight, string Financial_Year, decimal Commodity_Rate,
        decimal Net_Amount, decimal Sub_Amount, decimal Service_Tax_Perc, decimal Service_Tax_Amt, string Depositor_Category, decimal Rebate_Perc, decimal Rebate_Amt, int Rebate_on_Unit,
        string Khasra_Number, string Rin_Pustika_No, string Cast_Certificate_No, string Is_Rebate, string Created_Date, string Modified_Date, string Client_IP, decimal Per_Day_Rate,
        string Bill_Type, int BId, int Month, string Godown_Id, string Crop_Year)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "INSERT INTO tbl_Institution_Storage_Bill_Details_Test(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId,Crop_Year,Month,Godown_Id) " +
        //    "values('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Type_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + From_Date +
        //    "','" + To_Date + "','" + Packing_Type + "','" + Weight + "','" + Financial_Year + "','" + Commodity_Rate +
        //    "','" + Net_Amount + "','" + Sub_Amount + "','" + Service_Tax_Perc + "','" + Service_Tax_Amt +
        //    "','" + Depositor_Category + "','" + Rebate_Perc + "','" + Rebate_Amt + "','" + Rebate_on_Unit + "','" + Khasra_Number + "','" + Is_Rebate +
        //    "','" + Created_Date + "','" + Modified_Date + "','" + Client_IP + "','" + Per_Day_Rate + "','" + Rin_Pustika_No + "','" + Cast_Certificate_No +
        //    "','" + Bill_Type + "','" + BId + "','" + Crop_Year + "','" + Month + "','" + Godown_Id + "')";
        //SqlCommand cmd = new SqlCommand(qry, con);
        string qry = "INSERT INTO tbl_Institution_Storage_Bill_Details(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId,Crop_Year,Month,Godown_Id) " +
            "values('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Type_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + From_Date +
            "','" + To_Date + "','" + Packing_Type + "','" + Weight + "','" + Financial_Year + "','" + Commodity_Rate +
            "','" + Net_Amount + "','" + Sub_Amount + "','" + Service_Tax_Perc + "','" + Service_Tax_Amt +
            "','" + Depositor_Category + "','" + Rebate_Perc + "','" + Rebate_Amt + "','" + Rebate_on_Unit + "','" + Khasra_Number + "','" + Is_Rebate +
            "','" + Created_Date + "','" + Modified_Date + "','" + Client_IP + "','" + Per_Day_Rate + "','" + Rin_Pustika_No + "','" + Cast_Certificate_No +
            "','" + Bill_Type + "','" + BId + "','" + Crop_Year + "','" + Month + "','" + Godown_Id + "')";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        con.Close();
    }
    //For Adding values in tbl_Bill_PVT_Godown_Daily_Rent_Test (Test Table)
    [WebMethod]
    public void EDAddBillPVTGodownDailyRentCharges(string Bill_Number, string Dates, decimal Opening_Balance, decimal Receive_Bags, decimal Issue_Bags, decimal Closing_Balance, decimal Per_Day_Rate,
        decimal Total_Charges, string Created_Date, string Modified_Date, decimal Opening_Weight, decimal Receive_Weight, decimal Issue_Weight, decimal Closing_Weight)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "INSERT INTO tbl_Bill_PVT_Godown_Daily_Rent_Test(Bill_Number, Dates, Opening_Balance, Receive_Bags, Issue_Bags, Closing_Bag_Balance, Per_Day_Rate, Total_Charges, Created_Date, Modified_Date, Opening_Weight, Receive_Weight, Issue_Weight, Closing_Weight) values" +
        //    "('" + Bill_Number + "', '" + Dates + "', '" + Opening_Balance + "', '" + Receive_Bags + "', '" + Issue_Bags + "', '" + Closing_Balance + "', '" + Per_Day_Rate + "', '" + Total_Charges + "', getdate(), '', '" + Opening_Weight + "', '" + Receive_Weight + "', '" + Issue_Weight + "', '" + Closing_Weight + "')";
        //SqlCommand cmd = new SqlCommand(qry, con);
        string qry = "INSERT INTO tbl_Bill_PVT_Godown_Daily_Rent(Bill_Number, Dates, Opening_Balance, Receive_Bags, Issue_Bags, Closing_Bag_Balance, Per_Day_Rate, Total_Charges, Created_Date, Modified_Date, Opening_Weight, Receive_Weight, Issue_Weight, Closing_Weight) values" +
            "('" + Bill_Number + "', '" + Dates + "', '" + Opening_Balance + "', '" + Receive_Bags + "', '" + Issue_Bags + "', '" + Closing_Balance + "', '" + Per_Day_Rate + "', '" + Total_Charges + "', getdate(), '', '" + Opening_Weight + "', '" + Receive_Weight + "', '" + Issue_Weight + "', '" + Closing_Weight + "')";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        con.Close();
    }


    //Adding Instituition Steel Silo Godown Daily Rent Bill Details in dbo.tbl_Bill_Steel_Silo_Godown_Daily_Rent_Test (Test Table)
    [WebMethod]
    public void EDAddSteelSiloGodownDailyRent(string Bill_Number, string Godown_Id, string Commodity_Id, string Crop_Year, string Bill_Type,
        string Type_ID, string Dates, decimal Opening_Weight, decimal Receive_Weight, decimal Issue_Weight, decimal Closing_Weight,
        decimal Chargeable_Closing_Weight, int Opening_Balance, int Receive_Bags, int Issue_Bags, int Closing_Bag_Balance,
        decimal Per_Day_Rate, decimal Total_Charges, string Created_Date, string Created_By)

    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "INSERT INTO dbo.tbl_Bill_Steel_Silo_Godown_Daily_Rent_Test(Bill_Number,Godown_ID,Commodity_ID,Crop_Year,Bill_Type," +
        //             "Type_ID,Dates,Opening_Weight,Receive_Weight,Issue_Weight,Closing_Weight,Chargeable_Closing_Weight,Opening_Balance," +
        //             "Receive_Bags,Issue_Bags,Closing_Bag_Balance,Per_Day_Rate,Total_Charges,Created_Date,Created_By) " +
        //             "VALUES ( '" + Bill_Number + "','" + Godown_Id + "','" + Commodity_Id + "','" + Crop_Year + "','" + Bill_Type +
        //             "','" + Type_ID + "','" + Dates + "','" + Opening_Weight + "','" + Receive_Weight + "','" + Issue_Weight +
        //             "','" + Closing_Weight + "','" + Chargeable_Closing_Weight + "','" + Opening_Balance + "','" + Receive_Bags +
        //             "','" + Issue_Bags + "','" + Closing_Bag_Balance + "','" + Per_Day_Rate + "','" + Total_Charges + "','" +
        //             "','" + Created_Date + "','" + Created_By + "')";
        //SqlCommand cmd = new SqlCommand(qry, con);
        string qry = "INSERT INTO dbo.tbl_Bill_Steel_Silo_Godown_Daily_Rent(Bill_Number,Godown_ID,Commodity_ID,Crop_Year,Bill_Type," +
                     "Type_ID,Dates,Opening_Weight,Receive_Weight,Issue_Weight,Closing_Weight,Chargeable_Closing_Weight,Opening_Balance," +
                     "Receive_Bags,Issue_Bags,Closing_Bag_Balance,Per_Day_Rate,Total_Charges,Created_Date,Created_By) " +
                     "VALUES ( '" + Bill_Number + "','" + Godown_Id + "','" + Commodity_Id + "','" + Crop_Year + "','" + Bill_Type +
                     "','" + Type_ID + "','" + Dates + "','" + Opening_Weight + "','" + Receive_Weight + "','" + Issue_Weight +
                     "','" + Closing_Weight + "','" + Chargeable_Closing_Weight + "','" + Opening_Balance + "','" + Receive_Bags +
                     "','" + Issue_Bags + "','" + Closing_Bag_Balance + "','" + Per_Day_Rate + "','" + Total_Charges + "','" +
                     "','" + Created_Date + "','" + Created_By + "')";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        con.Close();

    }

    //Adding Instituition Steel Silo Godown Daily Rent Bill Details in tbl_Institution_Steel_Silo_Storage_Bill_Details_Test (Test Table)
    [WebMethod]
    public void EDAddInstitutionSteelSiloStorageBillDetails(string Bill_Number, string District_Id, string Branch_Id, string Depositor_Id,
        string Commodity_Id, string From_Date, string To_Date, string Financial_Year, decimal Commodity_Rate,
        decimal Net_Amount, decimal Sub_Amount, decimal Service_Tax_Perc, decimal Service_Tax_Amt, string Depositor_Category,
        decimal Rebate_Perc, decimal Rebate_Amt, int Rebate_on_Unit, string Is_Rebate, string Created_Date, string Client_IP, decimal Per_Day_Rate, int Bill_Type_ID,
        string Bill_Type, int BId, int Month, string Godown_Id, string Crop_Year, string Invoice_No, int Bill_Category_Type_ID)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        //string qry = "INSERT INTO tbl_Institution_Steel_Silo_Storage_Bill_Details_Test(Bill_Number,District_Id,Branch_Id,Depositor_Id,Commodity_Id," +
        //    "From_Date,To_Date,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category," +
        //    "Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Is_Rebate,Created_Date,Client_IP,Per_Day_Rate,Bill_Type,BId,Month,Godown_Id,Crop_Year," +
        //    "Packing_Type,Weight,Invoice_No,Bill_Category_Type_ID) " +
        //    "VALUES ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" +
        //    From_Date + "','" + To_Date + "','" + Financial_Year + "','" + Commodity_Rate + "','" + Net_Amount + "','" + Sub_Amount +
        //    "','" + Service_Tax_Perc + "','" + Service_Tax_Amt + "','" + Depositor_Category + "','" + Rebate_Perc + "','" + Rebate_Amt +
        //    "','" + Rebate_on_Unit + "','" + Is_Rebate + "','" + Created_Date + "','" + Client_IP + "','" + Per_Day_Rate +
        //    "','" + Bill_Type + "','" + Bill_Type_ID + "','" + BId + "','" + Month + "','" + Godown_Id + "','" + Crop_Year +
        //    "','" + "" + "','" + "" + "','" + Invoice_No + "','" + Bill_Category_Type_ID + "')";
        //SqlCommand cmd = new SqlCommand(qry, con);
        string qry = "INSERT INTO tbl_Institution_Steel_Silo_Storage_Bill_Details(Bill_Number,District_Id,Branch_Id,Depositor_Id,Commodity_Id," +
           "From_Date,To_Date,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category," +
           "Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Is_Rebate,Created_Date,Client_IP,Per_Day_Rate,Bill_Type,BId,Month,Godown_Id,Crop_Year," +
           "Packing_Type,Weight,Invoice_No,Bill_Category_Type_ID) " +
           "VALUES ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" +
           From_Date + "','" + To_Date + "','" + Financial_Year + "','" + Commodity_Rate + "','" + Net_Amount + "','" + Sub_Amount +
           "','" + Service_Tax_Perc + "','" + Service_Tax_Amt + "','" + Depositor_Category + "','" + Rebate_Perc + "','" + Rebate_Amt +
           "','" + Rebate_on_Unit + "','" + Is_Rebate + "','" + Created_Date + "','" + Client_IP + "','" + Per_Day_Rate +
           "','" + Bill_Type + "','" + Bill_Type_ID + "','" + BId + "','" + Month + "','" + Godown_Id + "','" + Crop_Year +
           "','" + "" + "','" + "" + "','" + Invoice_No + "','" + Bill_Category_Type_ID + "')";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        con.Close();

    }


    //For Deleting Final Bill Record for Steel Silo table and Digital Sign (Test Table) 
    [WebMethod]
    public void EDDeleteFinalBillSteelSilo(string Bill_Number, string Client_IP)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        SqlCommand cmd = new SqlCommand("dbo.Delete_Steel_Silo_Bill_Test", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Bill_Number", Bill_Number);
        cmd.Parameters.AddWithValue("@IPAddress", Client_IP);
        cmd.ExecuteNonQuery();
        con.Close();
    }

}

