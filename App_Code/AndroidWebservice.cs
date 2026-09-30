using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;

/// <summary>
/// Summary description for AndroidWebservice
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class AndroidWebservice : System.Web.Services.WebService {
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
   

    public AndroidWebservice () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //[WebMethod]
    //public string HelloWorld(string YourName) {
    //    return "Hello "+YourName;
    //}

    [WebMethod]
    //Mandilogin
    public bool checklogin_PvtGodown(string Userid, string Pwd)
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string str = "SELECT Godown_Name,Password  FROM [Pvt_Warehouse_Login] WHERE Godown_Name='" + Userid + "' AND Password='" + Pwd + "'";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

          
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    [WebMethod]
    public bool InsertReceivingStockData(string userid, string pwd, string DistrictId, string BranchId, string Godown_Id, string ChallanNo, string ReceiptID,string TruckNo, string S_Arrival, string Commodity, string DateDeposit, string category, string CropYear, string Transpoter, string WCMNO, string MOWgt, string QtySent, string BagsSent, string QtyRecd, string BagsRecd, string CreatedDate, string Ip, string IMEI, string ModelNo, string Board, string Brand, string SIMNO)
    {
        bool a;
        a = checklogin_PvtGodown(userid.ToString(), pwd.ToString());
        if (a == true)
        {
            try
            {
                string str = "SELECT * FROM [tbl_Android_ReceivingStock] WHERE DistrictId='" + DistrictId + "' AND BranchId='" + BranchId + "' AND ReceiptID='" + ReceiptID + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.CommandTimeout = 0;
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return false;
                }

                else
                {
                    #region SPCF_sp_Android_ReceiptStock
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }



                    SqlTransaction trans = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                    SqlCommand cmd_sp = new SqlCommand("sp_Android_ReceiptStock", con, trans);
                    cmd_sp.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cmd_sp.Parameters.AddWithValue("@AndRecId", SqlDbType.VarChar);
                        cmd_sp.Parameters.AddWithValue("@DistrictId", DistrictId);


                        cmd_sp.Parameters.AddWithValue("@BranchId", BranchId);
                        cmd_sp.Parameters.AddWithValue("@Godown_Id", Godown_Id);
                        cmd_sp.Parameters.AddWithValue("@ChallanNo ", ChallanNo);
                        cmd_sp.Parameters.AddWithValue("@ReceiptID", ReceiptID);
                     
                        cmd_sp.Parameters.AddWithValue("@TruckNo ", TruckNo);
                        cmd_sp.Parameters.AddWithValue("@S_Arrival", S_Arrival);
                        cmd_sp.Parameters.AddWithValue("@Commodity", Commodity);
                        cmd_sp.Parameters.AddWithValue("@DateDeposit", DateDeposit);
                        cmd_sp.Parameters.AddWithValue("@category", category);

                       
                    
                        cmd_sp.Parameters.AddWithValue("@CropYear", CropYear);
                        cmd_sp.Parameters.AddWithValue("@Transpoter", Transpoter);
                        cmd_sp.Parameters.AddWithValue("@WCMNO", WCMNO);

                        cmd_sp.Parameters.AddWithValue("@MOWgt", MOWgt);
                        cmd_sp.Parameters.AddWithValue("@QtySent", QtySent);
                        cmd_sp.Parameters.AddWithValue("@BagsSent", BagsSent);
                        cmd_sp.Parameters.AddWithValue("@QtyRecd", QtyRecd);

                        cmd_sp.Parameters.AddWithValue("@BagsRecd", BagsRecd);
                        cmd_sp.Parameters.AddWithValue("@CreatedDate", CreatedDate);

                      
                        cmd_sp.Parameters.AddWithValue("@Ip", Ip);
                       
                        cmd_sp.Parameters.AddWithValue("@IMEI", IMEI);
                        cmd_sp.Parameters.AddWithValue("@ModelNo", ModelNo);
                        cmd_sp.Parameters.AddWithValue("@Board", Board);
                        cmd_sp.Parameters.AddWithValue("@Brand ", Brand);
                        cmd_sp.Parameters.AddWithValue("@SIMNO", SIMNO);

                        cmd_sp.ExecuteNonQuery();
                        trans.Commit();
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                        return true;
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        trans.Rollback();
                        string msg = "Insert Error:";
                        msg += ex.Message;
                        throw new Exception(msg);
                    }
                    #endregion;
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Insert Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
        }
        else
        {
            return false;
        }
    }

    [WebMethod]
    public bool InsertReceivingStockProcurement(string userid, string pwd, string DistrictId, string BranchId, string Godown_Id, string DepositorNo, string S_Arrival, string Commodity, string DateDeposit, string CropYear, string WCMNO, string MOWgt, string QtySent, string BagsSent, string QtyRecd, string BagsRecd, string AvgMoisture_Content, string CreatedDate, string Ip, string IMEI, string ModelNo, string Board, string Brand, string SIMNO)
    {
        bool a;
        a = checklogin_PvtGodown(userid.ToString(), pwd.ToString());
        if (a == true)
        {
            try
            {
                string str = "SELECT * FROM [tbl_Android_ReceivingStockProcurement] WHERE DistrictId='" + DistrictId + "' AND BranchId='" + BranchId + "' AND DepositorNo='" + DepositorNo + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.CommandTimeout = 0;
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return false;
                }

                else
                {
                    #region SPCF_Android_ReceiptStockProcurement
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlTransaction trans = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                    SqlCommand cmd_sp = new SqlCommand("sp_Android_ReceiptStockProcurement", con, trans);
                    cmd_sp.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cmd_sp.Parameters.AddWithValue("@Depositor_WHR_Id", SqlDbType.VarChar);
                        cmd_sp.Parameters.AddWithValue("@State_ID", SqlDbType.VarChar);
                        cmd_sp.Parameters.AddWithValue("@DistrictId", DistrictId);


                        cmd_sp.Parameters.AddWithValue("@BranchId", BranchId);
                        cmd_sp.Parameters.AddWithValue("@Depositor_Name", SqlDbType.VarChar);
                        cmd_sp.Parameters.AddWithValue("@DepositorID", SqlDbType.VarChar);
                        cmd_sp.Parameters.AddWithValue("@Godown_Id", Godown_Id);
                   
                        cmd_sp.Parameters.AddWithValue("@DepositorNo", DepositorNo);
                        cmd_sp.Parameters.AddWithValue("@Whr_No", SqlDbType.VarChar);

                        cmd_sp.Parameters.AddWithValue("@Category_Id", SqlDbType.VarChar);
                        cmd_sp.Parameters.AddWithValue("@S_Arrival", S_Arrival);
                        cmd_sp.Parameters.AddWithValue("@Commodity", Commodity);
                        cmd_sp.Parameters.AddWithValue("@DateDeposit", DateDeposit);

                        cmd_sp.Parameters.AddWithValue("@CropYear", CropYear);
                  
                        cmd_sp.Parameters.AddWithValue("@WCMNO", WCMNO);

                        cmd_sp.Parameters.AddWithValue("@MOWgt", MOWgt);
                        cmd_sp.Parameters.AddWithValue("@QtySent", QtySent);
                        cmd_sp.Parameters.AddWithValue("@BagsSent", BagsSent);
                        cmd_sp.Parameters.AddWithValue("@QtyRecd", QtyRecd);

                        cmd_sp.Parameters.AddWithValue("@BagsRecd", BagsRecd);
                        cmd_sp.Parameters.AddWithValue("@AvgMoisture_Content", AvgMoisture_Content);
                        cmd_sp.Parameters.AddWithValue("@CreatedDate", CreatedDate);


                        cmd_sp.Parameters.AddWithValue("@Ip", Ip);

                        cmd_sp.Parameters.AddWithValue("@IMEI", IMEI);
                        cmd_sp.Parameters.AddWithValue("@ModelNo", ModelNo);
                        cmd_sp.Parameters.AddWithValue("@Board", Board);
                        cmd_sp.Parameters.AddWithValue("@Brand ", Brand);
                        cmd_sp.Parameters.AddWithValue("@SIMNO", SIMNO);

                        cmd_sp.ExecuteNonQuery();
                        trans.Commit();
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                        return true;
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        trans.Rollback();
                        string msg = "Insert Error:";
                        msg += ex.Message;
                        throw new Exception(msg);
                    }
                    #endregion;
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Insert Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
        }
        else
        {
            return false;
        }
    }



    [WebMethod]
    public XmlElement GetDetailsAndroid_ReceivingStockProcurement(string Godown_Id, string Whr_No, string BranchId)
    {
        try
        {
            XmlElement xmlElement = null;

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SELECT [Depositor_WHR_Id],[State_Id],[DistrictId],[BranchId],[Depositor_Name],[DepositorID],[Godown_Id],[DepositorNo],[Whr_No],[QtySent],[BagsSent],[QtyRecd],[BagsRecd]FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Android_ReceivingStockProcurement] where Godown_Id=@Godown_Id AND Whr_No=@Whr_No And BranchId=@BranchId ", con);


            cmd.Parameters.AddWithValue("@Godown_Id", Godown_Id);
            cmd.Parameters.AddWithValue("@Whr_No", Whr_No);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);

            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // Create an instance of DataSet.
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            string strm = string.Empty;
            if (ds.Tables[0].Rows.Count > 0)
            {
                // Return the DataSet as an XmlElement.
                XmlDataDocument xmldata = new XmlDataDocument(ds);
                xmlElement = xmldata.DocumentElement;
            }
            else
            {

            }
            return xmlElement;
        }
        catch (Exception)
        {
            throw new SoapException("false", SoapException.ClientFaultCode);
        }

    }


    [WebMethod]
    public bool InsertAndroid_deliveryorder(string userid, string pwd, string District_Id, string Godown_Id, string DepotId, string WHR_Id, string Delivery_Order_Date, string Qty_Issued_No_Bags_Sound, string Qty_Issued_Weight, string BranchID, string CreatedDate, string Ip, string IMEI, string ModelNo, string Board, string Brand, string SIMNO)
    {
        bool a;
        a = checklogin_PvtGodown(userid.ToString(), pwd.ToString());
        if (a == true)
        {
            try
            {
                string str = "SELECT * FROM [tbl_Storage_Android_Stock_Delivery_Order] WHERE District_Id ='" + District_Id + "' AND DepotId='" + DepotId + "' AND WHR_Id='" + WHR_Id + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.CommandTimeout = 0;
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return false;
                }

                else
                {
                    #region SPCF_Android_ReceiptStockProcurement
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlTransaction trans = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                    SqlCommand cmd_sp = new SqlCommand("sp_Android_deliveryorder_insert", con, trans);
                    cmd_sp.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cmd_sp.Parameters.AddWithValue("@StockDeliveryOrder_Id", SqlDbType.VarChar);
                        cmd_sp.Parameters.AddWithValue("@State_ID", SqlDbType.VarChar);
                        cmd_sp.Parameters.AddWithValue("@District_Id", District_Id);

                        cmd_sp.Parameters.AddWithValue("@Godown_Id", Godown_Id);
                        cmd_sp.Parameters.AddWithValue("@DepotId", DepotId);
                        cmd_sp.Parameters.AddWithValue("@WHR_Id", WHR_Id);
                        cmd_sp.Parameters.AddWithValue("@Delivery_Order_No ", SqlDbType.VarChar);
                        cmd_sp.Parameters.AddWithValue("@Delivery_Order_Date", Delivery_Order_Date);
                        cmd_sp.Parameters.AddWithValue("@Qty_Issued_No_Bags_Sound", Qty_Issued_No_Bags_Sound);
                        cmd_sp.Parameters.AddWithValue("@Qty_Issued_Weight ", Qty_Issued_Weight);
                        cmd_sp.Parameters.AddWithValue("@BranchID", BranchID);

                        cmd_sp.Parameters.AddWithValue("@CreatedDate", CreatedDate);

                        cmd_sp.Parameters.AddWithValue("@Ip", Ip);

                        cmd_sp.Parameters.AddWithValue("@IMEI", IMEI);
                        cmd_sp.Parameters.AddWithValue("@ModelNo", ModelNo);
                        cmd_sp.Parameters.AddWithValue("@Board", Board);
                        cmd_sp.Parameters.AddWithValue("@Brand ", Brand);
                        cmd_sp.Parameters.AddWithValue("@SIMNO", SIMNO);

                        cmd_sp.ExecuteNonQuery();
                        trans.Commit();
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                        return true;
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        trans.Rollback();
                        string msg = "Insert Error:";
                        msg += ex.Message;
                        throw new Exception(msg);
                    }
                    #endregion;
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Insert Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
        }
        else
        {
            return false;
        }
    }

    [WebMethod]
    public XmlElement GetDetailsForReceivingStockProcurement(string Godown_Id, string DepositorNo, string BranchId)
    {
        try
        {
            XmlElement xmlElement = null;

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SELECT [Depositor_WHR_Id],[State_Id],[DistrictId],[BranchId],[Depositor_Name],[DepositorID],[Godown_Id],[DepositorNo],[Whr_No],[QtySent],[BagsSent],[QtyRecd],[BagsRecd]FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Android_ReceivingStockProcurement] where Godown_Id=@Godown_Id AND DepositorNo=@DepositorNo And BranchId=@BranchId ", con);


            cmd.Parameters.AddWithValue("@Godown_Id", Godown_Id);
            cmd.Parameters.AddWithValue("@DepositorNo", DepositorNo);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);

            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // Create an instance of DataSet.
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            string strm = string.Empty;
            if (ds.Tables[0].Rows.Count > 0)
            {
                // Return the DataSet as an XmlElement.
                XmlDataDocument xmldata = new XmlDataDocument(ds);
                xmlElement = xmldata.DocumentElement;
            }
            else
            {

            }
            return xmlElement;
        }
        catch (Exception)
        {
            throw new SoapException("false", SoapException.ClientFaultCode);
        }

    }


    [WebMethod]
    public XmlElement GetDetailsForDeliveryOrder(string Godown_Id, string WHR_Id, string BranchID)
    {
        try
        {
            XmlElement xmlElement = null;

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SELECT [StockDeliveryOrder_Id],[State_Id],[District_Id],[Godown_Id],[DepotId],[WHR_Id],[Delivery_Order_No],[Delivery_Order_Date],[Qty_Issued_No_Bags_Sound],[Qty_Issued_Weight],[BranchID] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Storage_Android_Stock_Delivery_Order] where Godown_Id=@Godown_Id AND WHR_Id=@WHR_Id And BranchID=@BranchID ", con);


            cmd.Parameters.AddWithValue("@Godown_Id", Godown_Id);
            cmd.Parameters.AddWithValue("@WHR_Id", WHR_Id);
            cmd.Parameters.AddWithValue("@BranchID", BranchID);

            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // Create an instance of DataSet.
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            string strm = string.Empty;
            if (ds.Tables[0].Rows.Count > 0)
            {
                // Return the DataSet as an XmlElement.
                XmlDataDocument xmldata = new XmlDataDocument(ds);
                xmlElement = xmldata.DocumentElement;
            }
            else
            {

            }
            return xmlElement;
        }
        catch (Exception)
        {
            throw new SoapException("false", SoapException.ClientFaultCode);
        }

    }


    [WebMethod]
    public XmlElement GetNotificationForTotstockProcurmentQtyAmt(string Godown_Id,string BranchId)
    {
        try
        {
            XmlElement xmlElement = null;

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("select Isnull(sum([QtyRecd]),0) as Totalsumqty  FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Android_ReceivingStockProcurement] where Godown_Id=@Godown_Id And BranchId=@BranchId ", con);


            cmd.Parameters.AddWithValue("@Godown_Id", Godown_Id);
          
            cmd.Parameters.AddWithValue("@BranchId", BranchId);

            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // Create an instance of DataSet.
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            string strm = string.Empty;
            if (ds.Tables[0].Rows.Count > 0)
            {
                // Return the DataSet as an XmlElement.
                XmlDataDocument xmldata = new XmlDataDocument(ds);
                xmlElement = xmldata.DocumentElement;
            }
            else
            {

            }
            return xmlElement;
        }
        catch (Exception)
        {
            throw new SoapException("false", SoapException.ClientFaultCode);
        }

    }


    [WebMethod]
    public XmlElement GetNotificationForTotstockQtyAmt(string Godown_Id, string BranchId)
    {
        try
        {
            XmlElement xmlElement = null;

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("select Isnull(sum([QtyRecd]),0) as Totalsumqty  FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Android_ReceivingStock] where Godown_Id=@Godown_Id And BranchId=@BranchId ", con);


            cmd.Parameters.AddWithValue("@Godown_Id", Godown_Id);

            cmd.Parameters.AddWithValue("@BranchId", BranchId);

            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // Create an instance of DataSet.
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            string strm = string.Empty;
            if (ds.Tables[0].Rows.Count > 0)
            {
                // Return the DataSet as an XmlElement.
                XmlDataDocument xmldata = new XmlDataDocument(ds);
                xmlElement = xmldata.DocumentElement;
            }
            else
            {

            }
            return xmlElement;
        }
        catch (Exception)
        {
            throw new SoapException("false", SoapException.ClientFaultCode);
        }

    }


    [WebMethod]
    public XmlElement login_PvtGodown(string GodownName, string Pwd)
    {
        try
        {
            XmlElement xmlElement = null;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            SqlCommand cmd = new SqlCommand("SELECT (SELECT District_Name+'-'+District_Id FROM tbl_MetaData_DISTRICT a where a.District_Id=Pvt_Warehouse_Login.DistrictId) AS DISTRICTNAME ,DistrictId,[Login_Id],Godown_Name,[Godown_Id],(SELECT BranchName+'-'+BranchID  FROM MetaDataBranchWithIssueCenter mdi where mdi.BranchID=Pvt_Warehouse_Login.BranchID ) as BranchName,[BranchID],[DepotId],[Scope],[Access_Restrict],[GodownTypeId],[Active] FROM [Intergrated_MP_STORAGE].[dbo].[Pvt_Warehouse_Login] WHERE Godown_Name=@GodownName AND Password=@Pwd", con);
            cmd.Parameters.AddWithValue("@GodownName", GodownName);
            cmd.Parameters.AddWithValue("@Pwd", Pwd);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // Create an instance of DataSet.
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            string strm = string.Empty;
            if (ds.Tables[0].Rows.Count > 0)
            {
                // Return the DataSet as an XmlElement.
                XmlDataDocument xmldata = new XmlDataDocument(ds);
                xmlElement = xmldata.DocumentElement;
                
            }
           
            return xmlElement;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
        }
    }
}

