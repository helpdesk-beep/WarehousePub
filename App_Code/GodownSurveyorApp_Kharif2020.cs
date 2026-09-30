using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using System.Security.Principal;
using System.Globalization;
using System.IO;
using System.Web;

/// <summary>
/// Summary description for GodownSurveyorApp_Kharif2020
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class GodownSurveyorApp_Kharif2020 : System.Web.Services.WebService
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlConnection conjvs = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    [WebMethod]
    public XmlElement Surveyorlogin_GdnInsp_Kharif2020(string userid, string password, string Mobile_No,string srvPass )
    {
        XmlElement xmlElement = null;
        string respas = "";
        if (userid == "nic" && password == "nicgdn#insp@2020")
        {
            try
            {
                // string query = "SELECT [SurveyorID],[SurveyorName],[Designation] ,[MobileNumber],[District],[agency],[Role] FROM [SurveyorRegistration_Rabi2020] where District='" + dist + "' and MobileNumber='" + Mobile_No + "' and Password='" + srvPass + "' ";
                //string query = "SELECT [SurveyorID],[SurveyorName],[Designation] ,[MobileNumber],[District],Branch FROM [tbl_Registration_Inspection_officer] where District='" + dist + "' and MobileNumber='" + Mobile_No + "' and Password='" + srvPass + "' and IsActive='Y' ";
                // string query = "SELECT ol.OfficerName,ol.PF_ID,ol.O_Password FROM JointVentureScheme2018.dbo.Insp_Officer_login as ol join JointVentureScheme2018.dbo.tbl_metadata_Inspection_officer as mi on mi.PF_ID = ol.PF_ID where  mi.Per_MobileNo = '" + Mobile_No + "' and ol.O_Password = '" + srvPass + "'  and mi.IsActive = 'Y'";
              // string query = "SELECT ol.OfficerName as [SurveyorName],ol.PF_ID as [SurveyorID],Designation,Per_MobileNo as [MobileNumber] FROM Intergrated_MP_STORAGE.dbo.Insp_Officer_login as ol join Intergrated_MP_STORAGE.dbo.tbl_metadata_Inspection_officer as mi on mi.PF_ID = ol.PF_ID where mi.Per_MobileNo = '" + Mobile_No + "' and ol.O_Password = '" + srvPass + "'  and mi.IsActive = 'Y'";
               string query = "SELECT ol.OfficerName as [SurveyorName],ol.PF_ID as [SurveyorID],Designation,Per_MobileNo as [MobileNumber] FROM JointVentureScheme2018.dbo.Insp_Officer_login as ol join JointVentureScheme2018.dbo.tbl_metadata_Inspection_officer as mi on mi.PF_ID = ol.PF_ID where mi.Per_MobileNo = '" + Mobile_No + "' and ol.O_Password = '" + srvPass + "'  and mi.IsActive = 'Y'";
                //XmlElement xmlElement = null;

                SqlCommand cmd = new SqlCommand(query, conjvs);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                if (conjvs.State == ConnectionState.Closed)
                {
                    conjvs.Open();
                }
                da.Fill(ds);
                if (conjvs.State == ConnectionState.Open)
                { conjvs.Close(); }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    XmlDataDocument xmldata1 = new XmlDataDocument(ds);
                    xmlElement = xmldata1.DocumentElement;
                }
                else
                {
                    
                }
            }
           catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode);
            }
            finally
            {
                if (conjvs.State == ConnectionState.Open)
                { conjvs.Close(); }
            }
        }
        return xmlElement;
    }


    [WebMethod]
    public XmlElement Get_Branch_GdnInsp_Kharif2020(string userid, string password, string SurveyorID)
    {
        XmlElement xmlElement = null;
        if (userid == "nic" && password == "nicgdn#insp@2020")
        {
            try
            {

                //string query = "select Inspection_ID,PF_ID,(select Officer_Name  from tbl_metadata_Inspection_officer where PF_ID = ISD.PF_ID) as Officer_Name,(select district_name from tbl_metadata_district as MDDIS where MDDIS.District_id = ISD.District_ID) as distirct_name,(select Depotname from tbl_metadata_depot as MDD where MDD.branchID = ISD.Branch_ID) as Depotname, Inspection_Status,Insp_Period,Convert(varchar(10), Order_Date, 103) as Order_Date,ISD.District_ID,ISD.Branch_ID  from tbl_Inpection_Scheduled_Date as ISD where PF_ID = '" + SurveyorID + "'";
                //string query = "select Inspection_ID,PF_ID as SurveyorID,(select Officer_Name  from JointVentureScheme2018.dbo.tbl_metadata_Inspection_officer where PF_ID = ISD.PF_ID) as SurveyorName,(select district_name from JointVentureScheme2018.dbo.tbl_metadata_district as MDDIS where MDDIS.District_id = ISD.District_ID) as distirct_name,(select Depotname from JointVentureScheme2018.dbo.tbl_metadata_depot as MDD where MDD.branchID = ISD.Branch_ID) as Depotname,Inspection_Status,Insp_Period,Convert(varchar(10), Order_Date, 103) as Order_Date,ISD.District_ID,ISD.Branch_ID from JointVentureScheme2018.dbo.tbl_Inpection_Scheduled_Date as ISD where PF_ID = '" + SurveyorID + "'";
                //string query = "select isd.ID as Inspection_ID, isd.Employee_ID as SurveyorID,(select Officer_Name from JointVentureScheme2018.dbo.tbl_metadata_Inspection_officer where PF_ID = ISD.Employee_ID) as SurveyorName,(select district_name from JointVentureScheme2018.dbo.tbl_metadata_district as MDDIS where MDDIS.District_id = ISD.District_ID) as distirct_name,(select Depotname from JointVentureScheme2018.dbo.tbl_metadata_depot as MDD where MDD.branchID = ISD.Branch_ID) as Depotname,isd.Inspection_Status,convert(varchar(15), mn.MonthName) + '_' + convert(varchar(15), year(isd.Order_Date)) as Insp_Period,Convert(varchar(10), Order_Date, 103) as Order_Date,ISD.District_ID,ISD.Branch_ID from JointVentureScheme2018.dbo.Inspection_Scheduled_For_Officer as ISD join JointVentureScheme2018.dbo.Mst_Month_Name as mn on mn.MonthID = isd.Inspection_month_ID where isd.Employee_ID = '" + SurveyorID + "'";
                string query = "select isd.ID as Inspection_ID, isd.Employee_ID as SurveyorID,(select Officer_Name from JointVentureScheme2018.dbo.tbl_metadata_Inspection_officer where PF_ID = ISD.Employee_ID) as SurveyorName,(select district_name from JointVentureScheme2018.dbo.tbl_metadata_district as MDDIS where MDDIS.District_id = ISD.District_ID) as distirct_name,(select Depotname from JointVentureScheme2018.dbo.tbl_metadata_depot as MDD where MDD.branchID = ISD.Branch_ID) as Depotname,isd.Status as Inspection_Status,convert(varchar(15), mn.Month_Name) + '_' + convert(varchar(15), year(isd.Order_Date)) as Insp_Period,Convert(varchar(10), Order_Date, 103) as Order_Date,ISD.District_ID,ISD.Branch_ID from JointVentureScheme2018.dbo.Inspection_Scheduled_For_Officer as ISD join JointVentureScheme2018.dbo.Mst_Month_Name as mn on mn.ID = isd.Inspection_month_ID where isd.Employee_ID = '" + SurveyorID + "'";
                //XmlElement xmlElement = null;

                SqlCommand cmd = new SqlCommand(query, conjvs);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                if (conjvs.State == ConnectionState.Closed)
                {
                    conjvs.Open();
                }
                da.Fill(ds);
                if (conjvs.State == ConnectionState.Open)
                { conjvs.Close(); }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    XmlDataDocument xmldata = new XmlDataDocument(ds);
                    xmlElement = xmldata.DocumentElement;
                }
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode);
            }
            finally
            {
                if (conjvs.State == ConnectionState.Open)
                { conjvs.Close(); }
            }
        }
        return xmlElement;
    }

    //[WebMethod]
    //public XmlElement Get_Branch_GdnInsp_Kharif2020(string userid, string password, string SurveyorID)
    //{
    //    XmlElement xmlElement = null;
    //    if (userid == "nic" && password == "nicgdn#insp@2020")
    //    {
    //        try
    //        {
    //            //string query = "SELECT BranchId,DepotName FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_DEPOT] where DistrictId = '"+ District_Id + "' order by DepotName";
    //            //string query = "SELECT distinct BranchId,DepotName FROM[Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_DEPOT] as mp join[Intergrated_MP_STORAGE].[dbo].[tbl_SurveyorGodownMapping_Inspection] as gi on gi.Branch = mp.BranchId where DistrictId = '" + District_Id + "' and SurveyorID = '" + SurveyorID + "' order by DepotName";
    //            //string query = "SELECT distinct BranchId,DepotName FROM[JointVentureScheme2018].[dbo].[tbl_MetaData_DEPOT] as mp join[JointVentureScheme2018].[dbo].tbl_Inpection_Scheduled_Date as gi on gi.Branch_ID = mp.BranchId where gi.PF_ID = '"+ SurveyorID + "' order by DepotName";
    //            string query = "SELECT distinct BranchId,DepotName FROM[Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_DEPOT] as mp join[Intergrated_MP_STORAGE].[dbo].tbl_Inpection_Scheduled_Date as gi on gi.Branch_ID = mp.BranchId where gi.PF_ID = '" + SurveyorID + "' order by DepotName";
    //            //XmlElement xmlElement = null;

    //            SqlCommand cmd = new SqlCommand(query, con);
    //            SqlDataAdapter da = new SqlDataAdapter(cmd);
    //            DataSet ds = new DataSet();
    //            if (con.State == ConnectionState.Closed)
    //            {
    //                con.Open();
    //            }
    //            da.Fill(ds);
    //            if (con.State == ConnectionState.Open)
    //            { con.Close(); }
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                XmlDataDocument xmldata = new XmlDataDocument(ds);
    //                xmlElement = xmldata.DocumentElement;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new SoapException(ex.Message, SoapException.ClientFaultCode);
    //        }
    //        finally
    //        {
    //            if (con.State == ConnectionState.Open)
    //            { con.Close(); }
    //        }
    //    }
    //    return xmlElement;
    //}

    [WebMethod]
    public XmlElement BindGodown_GdnInsp_Kharif2020(string userid, string password, string Branch, string SurveyorID)
    {
        XmlElement xmlElement = null;
        if (userid == "nic" && password == "nicgdn#insp@2020")
        {
            try
            {

                //string query = "SELECT [Godown],SurveyorID, (Select Godown_name FROM[Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN_2018] where Godown_ID = Godown) as GodownName FROM[tbl_SurveyorGodownMapping_Inspection] as si where Dist = '" + dist + "' and Branch = '"+Branch+"' and SurveyorID = '" + SurveyorID + "' group by Godown,SurveyorID";
                //string query = "SELECT distinct SurveyorID,g18.Godown_ID,g18.Godown_Name FROM [tbl_SurveyorGodownMapping_Inspection] as si join[tbl_MetaData_GODOWN_2018] as g18 on g18.BranchID = si.Branch where Dist = '" + dist + "' and Branch = '" + Branch + "' and SurveyorID = '" + SurveyorID + "' group by g18.Godown_ID,g18.Godown_Name,SurveyorID";
                // string query = "SELECT distinct PF_ID,g18.Godown_ID,g18.Godown_Name FROM JointVentureScheme2018.dbo.tbl_Inpection_Scheduled_Date as si join JointVentureScheme2018.dbo.[tbl_MetaData_GODOWN_2018] as g18 on g18.BranchID = si.Branch_ID where si.Branch_ID = '"+Branch+"' and PF_ID = '"+ SurveyorID + "' group by g18.Godown_ID,g18.Godown_Name,PF_ID";
                //string query = "SELECT distinct PF_ID as SurveyorID,g18.Godown_ID,g18.Godown_Name FROM Intergrated_MP_STORAGE.dbo.tbl_Inpection_Scheduled_Date as si join Intergrated_MP_STORAGE.dbo.[tbl_MetaData_GODOWN_2018] as g18 on g18.BranchID = si.Branch_ID where si.Branch_ID = '" + Branch + "' and PF_ID = '" + SurveyorID + "' group by g18.Godown_ID,g18.Godown_Name,PF_ID";
               // string query = "SELECT distinct PF_ID as SurveyorID,g18.Godown_ID,g18.Godown_Name FROM JointVentureScheme2018.dbo.tbl_Inpection_Scheduled_Date as si join JointVentureScheme2018.dbo.[tbl_MetaData_GODOWN_2018] as g18 on g18.BranchID = si.Branch_ID where si.Branch_ID = '" + Branch + "' and PF_ID = '" + SurveyorID + "' group by g18.Godown_ID,g18.Godown_Name,PF_ID";
                string query = "SELECT distinct si.Employee_ID as SurveyorID,g18.Godown_ID,g18.Godown_Name FROM JointVentureScheme2018.dbo.Inspection_Scheduled_For_Officer as si join JointVentureScheme2018.dbo.[tbl_MetaData_GODOWN_2018] as g18 on g18.BranchID = si.Branch_ID where si.Branch_ID = '" + Branch + "' and si.Employee_ID = '" + SurveyorID + "' group by g18.Godown_ID,g18.Godown_Name,si.Employee_ID";
                //XmlElement xmlElement = null;

                SqlCommand cmd = new SqlCommand(query, conjvs);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                if (conjvs.State == ConnectionState.Closed)
                {
                    conjvs.Open();
                }
                da.Fill(ds);
                if (conjvs.State == ConnectionState.Open)
                { conjvs.Close(); }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    XmlDataDocument xmldata = new XmlDataDocument(ds);
                    xmlElement = xmldata.DocumentElement;
                }
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode);
            }
            finally
            {
                if (conjvs.State == ConnectionState.Open)
                { conjvs.Close(); }
            }
        }
        return xmlElement;
    }
    
     
    [WebMethod]
    public XmlElement GetGdnDetails_GdnInsp_Kharif2020(string userid, string password, string Godown_Id)
    {

        XmlElement xmlElement = null;
        if (userid == "nic" && password == "nicgdn#insp@2020")
        {
            // SqlCommand cmd = new SqlCommand();
            try
            {
                //cmd.Parameters.Clear();
                SqlCommand cmd = new SqlCommand("[SP_Get_Godown_Stack_Cmdty_Details]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Godown_Id", Godown_Id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.CommandTimeout = 0;
                DataSet ds_FAQ = new DataSet();
                SqlDataAdapter da_FAQ = new SqlDataAdapter(cmd);
                if (con.State == ConnectionState.Closed) { con.Open(); }
                da.Fill(ds_FAQ);
                if (con.State == ConnectionState.Open) { con.Close(); }
                if (ds_FAQ.Tables[0].Rows.Count > 0)
                {
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    XmlDataDocument xmldata = new XmlDataDocument(ds_FAQ);
                    xmlElement = xmldata.DocumentElement;
                    return xmlElement;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                throw new SoapException(ex.Message, SoapException.ClientFaultCode);
            }
            finally
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
            }
        }

        return xmlElement;
    }

    [WebMethod]
    public XmlElement GetStackDetails_StackInsp_Kharif2020(string userid, string password, string Godown_Id)
    {

        XmlElement xmlElement = null;
        if (userid == "nic" && password == "nicgdn#insp@2020")
        {
            // SqlCommand cmd = new SqlCommand();
            try
            {
                //cmd.Parameters.Clear();
                SqlCommand cmd = new SqlCommand("SP_Get_Godown_Stackwise_Cmdty_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Godown_Id", Godown_Id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.CommandTimeout = 0;
                DataSet ds_FAQ = new DataSet();
                SqlDataAdapter da_FAQ = new SqlDataAdapter(cmd);
                if (con.State == ConnectionState.Closed) { con.Open(); }
                da.Fill(ds_FAQ);
                if (con.State == ConnectionState.Open) { con.Close(); }
                if (ds_FAQ.Tables[0].Rows.Count > 0)
                {
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    XmlDataDocument xmldata = new XmlDataDocument(ds_FAQ);
                    xmlElement = xmldata.DocumentElement;
                    return xmlElement;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                throw new SoapException(ex.Message, SoapException.ClientFaultCode);
            }
            finally
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
            }
        }

        return xmlElement;
    }


    [WebMethod]
    public string InsertRECFAQ_GdnInsp_Kharif2020(string userid, string password, string DistrictId, string BranchId, string Godown_ID, string DepositorID, string Depositor_Name, string No_Of_Stack, string Inspection_Date, string Last_Inspection_Date, string SurveyorID, string Commodity, string Available_Bags, string Available_Qty, string PV_No_Of_Bags, string WithInsect, string category, string Moisture, string No_Of_Bags_Spillage, string Page_Number, string Latitude, string Longitude, string IMEI_Number)
    {
        string finalMassege = "";
        string SuccessFlag;
        string ApplicationID;
        if (userid == "nic" && password == "nicgdn#insp@2020")
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Clear();
                cmd.CommandText = "[SP_InsertInto_tbl_Gdn_Inspection_Details_WH]";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add("@Inspection_Ref_Id", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;


                cmd.Parameters.Add("@DistrictId", SqlDbType.VarChar, 4).Value = DistrictId;
                cmd.Parameters.Add("@BranchId", SqlDbType.VarChar, 20).Value = BranchId;
                cmd.Parameters.Add("@Godown_ID", SqlDbType.VarChar, 20).Value = Godown_ID;
                // cmd.Parameters.Add("@Godown_Capacity", SqlDbType.Float).Value = Godown_Capacity;
                cmd.Parameters.Add("@DepositorID", SqlDbType.VarChar, 20).Value = DepositorID;
                cmd.Parameters.Add("@Depositor_Name", SqlDbType.VarChar, 100).Value = Depositor_Name;
                cmd.Parameters.Add("@No_Of_Stack", SqlDbType.Int).Value = No_Of_Stack;
                cmd.Parameters.Add("@Inspection_Date", SqlDbType.DateTime).Value = Inspection_Date;
                cmd.Parameters.Add("@Last_Inspection_Date", SqlDbType.DateTime).Value = Last_Inspection_Date;
                cmd.Parameters.Add("@PF_ID", SqlDbType.VarChar, 20).Value = SurveyorID;
                //cmd.Parameters.Add("@CropYear", SqlDbType.VarChar, 10).Value = CropYear;
                cmd.Parameters.Add("@Commodity", SqlDbType.VarChar, 10).Value = Commodity;
                cmd.Parameters.Add("@Available_Bags", SqlDbType.Int).Value = Available_Bags;
                cmd.Parameters.Add("@Available_Qty", SqlDbType.Float).Value = Available_Qty;
                cmd.Parameters.Add("@PV_No_Of_Bags", SqlDbType.Int).Value = PV_No_Of_Bags;
                //cmd.Parameters.Add("@PV_No_Of_Bags_diff", SqlDbType.Int).Value = PV_No_Of_Bags_difference;
                cmd.Parameters.Add("@IsInsect", SqlDbType.VarChar, 1).Value = WithInsect;
                cmd.Parameters.Add("@Category", SqlDbType.VarChar, 1).Value = category;
                cmd.Parameters.Add("@Moisture", SqlDbType.Float).Value = Moisture;
                cmd.Parameters.Add("@No_Of_Bags_Spillage", SqlDbType.Int).Value = No_Of_Bags_Spillage;
                cmd.Parameters.Add("@Page_Number", SqlDbType.Int).Value = Page_Number;
                cmd.Parameters.Add("@Latitude", SqlDbType.Float).Value = Latitude;
                cmd.Parameters.Add("@Longitude", SqlDbType.Float).Value = Longitude;
                cmd.Parameters.Add("@IMEI_Number", SqlDbType.VarChar, 20).Value = IMEI_Number;
                //cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = CreatedDate;

                cmd.Parameters.Add("@flag", SqlDbType.VarChar, 5).Direction = ParameterDirection.Output;

                if (con.State == ConnectionState.Closed) { con.Open(); }
                cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open) { con.Close(); }
                SuccessFlag = Convert.ToString(Convert.ToBoolean(cmd.Parameters["@Flag"].Value));
                ApplicationID = Convert.ToString(cmd.Parameters["@Inspection_Ref_Id"].Value);
                finalMassege = SuccessFlag + ApplicationID;
                return finalMassege;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                return ex.ToString();
                throw new SoapException(ex.Message, SoapException.ClientFaultCode);
            }
            finally
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
            }
        }
        return "00";
    }

    [WebMethod]
    public string InsertRECFAQ_Gdn_Stack_Insp_Kharif2020(string userid, string password, string DistrictId, string BranchId, string Godown_ID, string DepositorID, string Depositor_Name, string Stack_Id, string Stack_Name, string Lenth,string Widht, string Hight_of_Bags_Layer, string Extra_Bags,string Other_Bags_Upper,string Other_Bags_Lower, string Category_Infested,string Block_Number, string Inspection_Date, string Last_Inspection_Date, string SurveyorID, string Commodity, string Available_Bags, string Available_Qty, string PV_No_Of_Bags, string WithInsect, string category, string Moisture, string No_Of_Bags_Spillage, string Page_Number, string Latitude, string Longitude, string IMEI_Number)
    {
        string finalMassege = "";
        string SuccessFlag;
        string ApplicationID;
        if (userid == "nic" && password == "nicgdn#insp@2020")
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Clear();
                cmd.CommandText = "[SP_InsertInto_tbl_Gdn_Stack_Inspection_Details_WH]";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add("@Inspection_Ref_Id", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;


                cmd.Parameters.Add("@DistrictId", SqlDbType.VarChar, 4).Value = DistrictId;
                cmd.Parameters.Add("@BranchId", SqlDbType.VarChar, 20).Value = BranchId;
                cmd.Parameters.Add("@Godown_ID", SqlDbType.VarChar, 20).Value = Godown_ID;
                cmd.Parameters.Add("@DepositorID", SqlDbType.VarChar, 20).Value = DepositorID;
                cmd.Parameters.Add("@Depositor_Name", SqlDbType.VarChar, 100).Value = Depositor_Name;
                cmd.Parameters.Add("@Stack_Id", SqlDbType.VarChar, 20).Value = Stack_Id;
                cmd.Parameters.Add("@Stack_Name", SqlDbType.VarChar, 50).Value = Stack_Name;
                cmd.Parameters.Add("@Lenth", SqlDbType.Decimal).Value = Lenth;
                cmd.Parameters.Add("@Widht", SqlDbType.Decimal).Value = Widht;
                cmd.Parameters.Add("@Hight_of_Bags_Layer", SqlDbType.Decimal).Value = Hight_of_Bags_Layer;
                cmd.Parameters.Add("@Extra_Bags", SqlDbType.Int).Value = Extra_Bags;
                cmd.Parameters.Add("@Other_Bags_Upper", SqlDbType.Int).Value = Other_Bags_Upper;
                cmd.Parameters.Add("@Other_Bags_Lower", SqlDbType.Int).Value = Other_Bags_Lower;
                cmd.Parameters.Add("@Category_Infested", SqlDbType.VarChar,1).Value = Category_Infested;
                cmd.Parameters.Add("@Block_Number", SqlDbType.Int).Value = Block_Number; 
                cmd.Parameters.Add("@Inspection_Date", SqlDbType.DateTime).Value = Inspection_Date;
                cmd.Parameters.Add("@Last_Inspection_Date", SqlDbType.DateTime).Value = Last_Inspection_Date;
                cmd.Parameters.Add("@PF_ID", SqlDbType.VarChar, 20).Value = SurveyorID;
                //cmd.Parameters.Add("@Crop_Year", SqlDbType.VarChar, 10).Value = Crop_Year;
                cmd.Parameters.Add("@Commodity", SqlDbType.VarChar, 10).Value = Commodity;
                cmd.Parameters.Add("@Available_Bags", SqlDbType.Int).Value = Available_Bags;
                cmd.Parameters.Add("@Available_Qty", SqlDbType.Float).Value = Available_Qty;
                cmd.Parameters.Add("@PV_No_Of_Bags", SqlDbType.Int).Value = PV_No_Of_Bags;
                cmd.Parameters.Add("@IsInsect", SqlDbType.VarChar, 1).Value = WithInsect;
                cmd.Parameters.Add("@Category", SqlDbType.VarChar, 1).Value = category;
                cmd.Parameters.Add("@Moisture", SqlDbType.Float).Value = Moisture;
                cmd.Parameters.Add("@No_Of_Bags_Spillage", SqlDbType.Int).Value = No_Of_Bags_Spillage;
                cmd.Parameters.Add("@Page_Number", SqlDbType.Int).Value = Page_Number;
                cmd.Parameters.Add("@Latitude", SqlDbType.Float).Value = Latitude;
                cmd.Parameters.Add("@Longitude", SqlDbType.Float).Value = Longitude;
                cmd.Parameters.Add("@IMEI_Number", SqlDbType.VarChar, 20).Value = IMEI_Number;
               

                cmd.Parameters.Add("@flag", SqlDbType.VarChar, 5).Direction = ParameterDirection.Output;

                if (con.State == ConnectionState.Closed) { con.Open(); }
                cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open) { con.Close(); }
                SuccessFlag = Convert.ToString(Convert.ToBoolean(cmd.Parameters["@Flag"].Value));
                ApplicationID = Convert.ToString(cmd.Parameters["@Inspection_Ref_Id"].Value);
                finalMassege = SuccessFlag + ApplicationID;
                return finalMassege;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                return ex.ToString();
                throw new SoapException(ex.Message, SoapException.ClientFaultCode);
            }
            finally
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
            }
        }
        return "00";
    }

    [WebMethod]
    public string InsertREC_Remark_GdnInsp_Kharif2020(string userid, string password, string Inspection_Ref_Id, string Remark)
    {
        string finalMassege = "";
        string SuccessFlag;
        //string ApplicationID;
        if (userid == "nic" && password == "nicgdn#insp@2020")
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Clear();
                cmd.CommandText = "[SP_InsertInto_tbl_Remark_Godown_Inspection]";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@Inspection_Ref_Id", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Inspection_Ref_Id", SqlDbType.VarChar, 50).Value = Inspection_Ref_Id;
                cmd.Parameters.Add("@Remark", SqlDbType.VarChar, 250).Value = Remark;
                //cmd.Parameters.Add("@IMEI_Number", SqlDbType.VarChar, 20).Value = IMEI_Number;
                //cmd.Parameters.Add("@Created_Date", SqlDbType.DateTime).Value = Created_Date;

                cmd.Parameters.Add("@flag", SqlDbType.VarChar, 5).Direction = ParameterDirection.Output;

                if (con.State == ConnectionState.Closed) { con.Open(); }
                cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open) { con.Close(); }
                SuccessFlag = Convert.ToString(Convert.ToBoolean(cmd.Parameters["@Flag"].Value));
                // ApplicationID = Convert.ToString(cmd.Parameters["@Inspection_Ref_Id"].Value);
                // finalMassege = SuccessFlag + ApplicationID;
                return finalMassege;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                return ex.ToString();
                throw new SoapException(ex.Message, SoapException.ClientFaultCode);
            }
            finally
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
            }
        }
        return "00";
    }

    [WebMethod]
    public string InsertREC_Remark_Gdn_Stack_Insp_Kharif2020(string userid, string password, string Inspection_Ref_Id, string Remark)
    {
        string finalMassege = "";
        string SuccessFlag;
        //string ApplicationID;
        if (userid == "nic" && password == "nicgdn#insp@2020")
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Clear();
                cmd.CommandText = "[SP_InsertInto_tbl_Remark_Godown_Stack_Inspection]";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@Inspection_Ref_Id", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Inspection_Ref_Id", SqlDbType.VarChar, 50).Value = Inspection_Ref_Id;
                cmd.Parameters.Add("@Remark", SqlDbType.VarChar, 250).Value = Remark;
                //cmd.Parameters.Add("@IMEI_Number", SqlDbType.VarChar, 20).Value = IMEI_Number;
                //cmd.Parameters.Add("@Created_Date", SqlDbType.DateTime).Value = Created_Date;

                cmd.Parameters.Add("@flag", SqlDbType.VarChar, 5).Direction = ParameterDirection.Output;

                if (con.State == ConnectionState.Closed) { con.Open(); }
                cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open) { con.Close(); }
                SuccessFlag = Convert.ToString(Convert.ToBoolean(cmd.Parameters["@Flag"].Value));
                // ApplicationID = Convert.ToString(cmd.Parameters["@Inspection_Ref_Id"].Value);
                // finalMassege = SuccessFlag + ApplicationID;
                finalMassege = "True";
                return finalMassege;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                return ex.ToString();
                throw new SoapException(ex.Message, SoapException.ClientFaultCode);
            }
            finally
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
            }
        }
        return "00";
    }

    //Method for rice
    [WebMethod]
    public XmlElement Surveyorlogin(string dist, string srvPass, string Mobile_No, string userid, string password)
    {
        XmlElement xmlElement = null;
        string respas = "";
        if (userid == "nic" && password == "nic2019")
        {
            try
            {
                // string query = "SELECT [SurveyorID],[SurveyorName],[Designation] ,[MobileNumber],[District],[agency],[Role] FROM [SurveyorRegistration_Rabi2020] where District='" + dist + "' and MobileNumber='" + Mobile_No + "' and Password='" + srvPass + "' ";
                // string query = "SELECT [SurveyorID],[SurveyorName],[Designation] ,[MobileNumber],[District],[agency] FROM [tbl_Registration_Inspection_officer] where District='" + dist + "' and MobileNumber='" + Mobile_No + "' and Password='" + srvPass + "' and IsActive='Y' ";
                string query = "SELECT [SurveyorID],[SurveyorName],[Designation] ,[MobileNumber],[District],Branch FROM [tbl_Registration_Inspection_officer] where District='" + dist + "' and MobileNumber='" + Mobile_No + "' and Password='" + srvPass + "' and IsActive='Y' ";
                //XmlElement xmlElement = null;

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                da.Fill(ds);
                if (con.State == ConnectionState.Open)
                { con.Close(); }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    XmlDataDocument xmldata1 = new XmlDataDocument(ds);
                    xmlElement = xmldata1.DocumentElement;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                { con.Close(); }
            }
        }
        return xmlElement;
    }

    //method for rice
    [WebMethod]
    public XmlElement GetFAQPara(string cropcode, string userid, string password)
    {
        XmlElement xmlElement = null;
        if (userid == "nic" && password == "nic2019")
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Parameters.Clear();
                string str = "Select SCCod,Special_Char‌_Unicode,MaxLimit_Tol from CropWise_FAQ Where crpCode='" + cropcode + "' Order By SCCod";
                cmd = new SqlCommand(str, con);
                cmd.CommandTimeout = 0;
                DataSet ds_FAQ = new DataSet();
                SqlDataAdapter da_FAQ = new SqlDataAdapter(cmd);
                if (con.State == ConnectionState.Closed) { con.Open(); }
                da_FAQ.Fill(ds_FAQ);
                if (con.State == ConnectionState.Open) { con.Close(); }
                if (ds_FAQ.Tables[0].Rows.Count > 0)
                {
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    XmlDataDocument xmldata = new XmlDataDocument(ds_FAQ);
                    xmlElement = xmldata.DocumentElement;
                    return xmlElement;
                }
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open) { con.Close(); }


                //throw new SoapException(ex.Message, SoapException.ClientFaultCode);

            }
            finally
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
            }

        }
        return xmlElement;
    }
        

    //method for rice
    [WebMethod]
    public XmlElement BindGodown(string dist, int SurveyorID, string userid, string password, string Branch)
    {
        XmlElement xmlElement = null;
        if (userid == "nic" && password == "nic2019")
        {
            try
            {

                string query = "SELECT [Godown],SurveyorID, (Select Godown_name FROM[Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN_2018] where Godown_ID = Godown) as GodownName FROM[tbl_SurveyorGodownMapping_Inspection] as si where Dist = '" + dist + "'  and SurveyorID = '" + SurveyorID + "' group by Godown,SurveyorID";
                //XmlElement xmlElement = null;

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                da.Fill(ds);
                if (con.State == ConnectionState.Open)
                { con.Close(); }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    XmlDataDocument xmldata = new XmlDataDocument(ds);
                    xmlElement = xmldata.DocumentElement;
                }

            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                { con.Close(); }

            }
        }
        return xmlElement;
    }
       

    //method for rice
    [WebMethod]
    public XmlElement GetGdnDetails_Kharif2020(string userid, string password, string Godown_Id)
    {

        XmlElement xmlElement = null;
        if (userid == "nic" && password == "nic2019")
        {
            // SqlCommand cmd = new SqlCommand();
            try
            {
                //cmd.Parameters.Clear();
                SqlCommand cmd = new SqlCommand("SP_Get_Godown_Stack_Commodity_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Godown_Id", Godown_Id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.CommandTimeout = 0;
                DataSet ds_FAQ = new DataSet();
                SqlDataAdapter da_FAQ = new SqlDataAdapter(cmd);
                if (con.State == ConnectionState.Closed) { con.Open(); }
                da_FAQ.Fill(ds_FAQ);
                if (con.State == ConnectionState.Open) { con.Close(); }
                if (ds_FAQ.Tables[0].Rows.Count > 0)
                {
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    XmlDataDocument xmldata = new XmlDataDocument(ds_FAQ);
                    xmlElement = xmldata.DocumentElement;
                    return xmlElement;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                throw new SoapException(ex.Message, SoapException.ClientFaultCode);
            }
            finally
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
            }
        }

        return xmlElement;
    }

    //method for rice
    [WebMethod]
    public XmlElement Get_Category(string userid, string password)
    {
        XmlElement xmlElement = null;
        if (userid == "nic" && password == "nic2019")
        {
            try
            {
                string query = "SELECT [Category_Id],[Category_Name]  FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_STORAGE_CATEGORY]";
                //XmlElement xmlElement = null;

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                da.Fill(ds);
                if (con.State == ConnectionState.Open)
                { con.Close(); }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    XmlDataDocument xmldata = new XmlDataDocument(ds);
                    xmlElement = xmldata.DocumentElement;
                }
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                { con.Close(); }
            }
        }
        return xmlElement;
    }

   
    //method for rice

    [WebMethod]
    public string InsertRECFAQ_Para( string userid, string password, string Godown_ID, string DistrictId, string Godown_Name, string Hired_Type, string Storage_Type, decimal Godown_Capacity, DateTime Inspection_Date, int SurveyorID, string CropYear, string Commodity, string stack_Id, string Stack_Name, int Available_Bags, decimal Available_Qty, decimal Broken, decimal Small_Broken, decimal Alien_elements, decimal Damaged_Daane, decimal Colorless_Daane, decimal Chaaki_Daane, decimal Red_Daane, decimal moisture, decimal WithInsect, decimal WithoutInsect, string Remarks, decimal Latitude, decimal Longitude, string IMEI_Number, DateTime CreatedDate)
    {
        string finalMassege = "";
        string SuccessFlag;
        string ApplicationID;
        if (userid == "nic" && password == "nic2019")
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Clear();
                cmd.CommandText = "InsertInto_tbl_Inspection_Details_WH";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                
                cmd.Parameters.Add("@Inspection_Ref_Id", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Godown_ID", SqlDbType.VarChar, 20).Value = Godown_ID;
                cmd.Parameters.Add("@DistrictId", SqlDbType.VarChar, 4).Value = DistrictId;
                cmd.Parameters.Add("@Godown_Name", SqlDbType.VarChar, 100).Value = Godown_Name;
                cmd.Parameters.Add("@Hired_Type", SqlDbType.VarChar, 50).Value = Hired_Type;
                cmd.Parameters.Add("@Storage_Type", SqlDbType.VarChar, 50).Value = Storage_Type;
                cmd.Parameters.Add("@Godown_Capacity", SqlDbType.Float).Value = Godown_Capacity;               
                cmd.Parameters.Add("@Inspection_Date", SqlDbType.DateTime).Value = Inspection_Date;
                cmd.Parameters.Add("@SurveyorID", SqlDbType.Int).Value = SurveyorID;
                cmd.Parameters.Add("@CropYear", SqlDbType.VarChar,10).Value = CropYear;
                cmd.Parameters.Add("@Commodity", SqlDbType.VarChar, 10).Value = Commodity;
                cmd.Parameters.Add("@stack_Id", SqlDbType.VarChar, 50).Value = stack_Id;
                cmd.Parameters.Add("@Stack_Name", SqlDbType.VarChar, 20).Value = Stack_Name;
                cmd.Parameters.Add("@Available_Bags", SqlDbType.Int).Value = Available_Bags;
                cmd.Parameters.Add("@Available_Qty", SqlDbType.Float).Value = Available_Qty;
                cmd.Parameters.Add("@Broken", SqlDbType.Float).Value = Broken;
                cmd.Parameters.Add("@Small_Broken", SqlDbType.Float).Value = Small_Broken;
                cmd.Parameters.Add("@Alien_elements", SqlDbType.Float).Value = Alien_elements;
                cmd.Parameters.Add("@Damaged_Daane", SqlDbType.Float).Value = Damaged_Daane;
                cmd.Parameters.Add("@Colorless_Daane", SqlDbType.Float).Value = Colorless_Daane;
                cmd.Parameters.Add("@Chaaki_Daane", SqlDbType.Float).Value = Chaaki_Daane;
                cmd.Parameters.Add("@Red_Daane", SqlDbType.Float, 25).Value= Red_Daane;
                cmd.Parameters.Add("@moisture", SqlDbType.Float).Value = moisture;
                cmd.Parameters.Add("@WithInsect", SqlDbType.Float).Value = WithInsect;
                cmd.Parameters.Add("@WithoutInsect", SqlDbType.Float).Value = WithoutInsect;                
                cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 200).Value = Remarks;
                cmd.Parameters.Add("@Latitude", SqlDbType.Float).Value = Latitude;
                cmd.Parameters.Add("@Longitude", SqlDbType.Float).Value = Longitude;
                cmd.Parameters.Add("@IMEI_Number", SqlDbType.VarChar, 20).Value = IMEI_Number;
                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = CreatedDate;
               
                cmd.Parameters.Add("@flag", SqlDbType.VarChar, 5).Direction = ParameterDirection.Output;

                if (con.State == ConnectionState.Closed) { con.Open(); }
                cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open) { con.Close(); }
                SuccessFlag = Convert.ToString(Convert.ToBoolean(cmd.Parameters["@Flag"].Value));
                ApplicationID = Convert.ToString(cmd.Parameters["@Inspection_Ref_Id"].Value);
                finalMassege = SuccessFlag + ApplicationID;               
                return finalMassege;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                return ex.ToString();
                throw new SoapException(ex.Message, SoapException.ClientFaultCode);
            }
            finally
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
            }
        }
        return "00";
    }

    
}


