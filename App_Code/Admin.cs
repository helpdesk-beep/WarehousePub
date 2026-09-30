using BAL;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace WLCBusinessLayer
{
    public class Admin
    {
        DataTable dt;
        SqlParameter param = null;
        ArrayList list = new ArrayList();
        int rvalue = 0;
        string strSql;
        //Change Password
        public int AdminChangePassword(string uid, string curPwd, string newPwd)
        {
            param = new SqlParameter("@Username", uid);
            list.Add(param);

            param = new SqlParameter("@CurrentPass", curPwd);
            list.Add(param);

            param = new SqlParameter("@NewPass", newPwd);
            list.Add(param);

            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"spAdminChangePassword", list, true);
            }

            catch (Exception)
            { }
            finally { }


            return rvalue;
        }


        //Current News
        public int SaveNews(string title, string desc, string filename)
        {
            param = new SqlParameter("@title", title);
            list.Add(param);

            param = new SqlParameter("@desc", desc);
            list.Add(param);

            param = new SqlParameter("@filename", filename);
            list.Add(param);

            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"spSaveNewsMaster", list, true);
            }

            catch (Exception ex)
            { }
            finally { }


            return rvalue;
        }

        public int EditNewsById(int id, string title, string desc, string filename)
        {
            param = new SqlParameter("@id", id);
            list.Add(param);

            param = new SqlParameter("@title", title);
            list.Add(param);

            param = new SqlParameter("@desc", desc);
            list.Add(param);

            param = new SqlParameter("@filename", filename);
            list.Add(param);

            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"spEditNewsById", list, true);
            }

            catch (Exception ex)
            { }
            finally { }


            return rvalue;
        }


        public DataTable GetNewsList()
        {
            dt = new DataTable();

            try
            {
                dt = new Sqldatalayer().SelectData(null,"spGetNewsMasterList", null, true);
            }
            catch (Exception ex)
            { }
            finally { }

            return dt;
        }

        public DataTable GetNewsById(int id)
        {
            SqlParameter param = new SqlParameter();

            param = new SqlParameter("@Id", id);
            list.Add(param);

            try
            {
                dt = new Sqldatalayer().SelectData(null,"[spGetNewsById]", list, true);
            }
            catch (Exception ex)
            { }
            finally { }

            return dt;
        }


        public int DeleteNewsById(int id)
        {
            param = new SqlParameter("@Id", id);
            list.Add(param);

            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"[spDeleteNewsById]", list, true);
            }
            catch (Exception ex)
            { }
            finally { }

            return rvalue;
        }



        //Section

        public DataTable GetSection()
        {
            try
            {
                dt = new Sqldatalayer().SelectData(null,"spGetSection", null, true);

            }

            catch (Exception ex) { }
            finally { }

            return dt;
        }

        // Tender
        public int SaveTender(int secid, string title, string filename, string expiry)
        {
            param = new SqlParameter("@secid", secid);
            list.Add(param);

            param = new SqlParameter("@title", title);
            list.Add(param);

            param = new SqlParameter("@filename", filename);
            list.Add(param);

            param = new SqlParameter("@expiry", expiry);
            list.Add(param);

            try
            {
               rvalue = new Sqldatalayer().ExecuteNonQuery(null,"[spSaveTender]", list, true);
            }

            catch (Exception ex) { }
            finally { }

            return rvalue;
        }

        public DataTable GetTenderList()
        {
            try
            {
                dt = new Sqldatalayer().SelectData(null,"[spGetTender]", null, true);
            }
            catch (Exception ex) { }
            finally { }

            return dt;
        }

        public DataTable GetTenderById(int id)
        {

            dt = new DataTable();

            param = new SqlParameter("@Id", id);
            list.Add(param);

            try
            {
                dt = new Sqldatalayer().SelectData(null,"[spGetTenderById]", list, true);
            }

            catch (Exception ex) { }
            finally { }

            return dt;
        }

        public int EditTenderById(int id, int secid, string title, string filename, string expiry)
        {
            param = new SqlParameter("@id", id);
            list.Add(param);

            param = new SqlParameter("@secid", secid);
            list.Add(param);

            param = new SqlParameter("@title", title);
            list.Add(param);

            param = new SqlParameter("@filename", filename);
            list.Add(param);

            param = new SqlParameter("@expiry", expiry);
            list.Add(param);


            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"[spEditTenderById]", list, true);
            }

            catch (Exception ex)
            { }
            finally { }


            return rvalue;
        }

        public int DeleteTenderById(int id)
        {
            param = new SqlParameter("@Id", id);
            list.Add(param);

            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"[spDeleteTenderById]", list, true);
            }
            catch (Exception ex)
            { }
            finally { }

            return rvalue;
        }



        // Agreement
        public int SaveAgreement(int secid, string title, string filename, string expiry)
        {
            param = new SqlParameter("@secid", secid);
            list.Add(param);

            param = new SqlParameter("@title", title);
            list.Add(param);

            param = new SqlParameter("@filename", filename);
            list.Add(param);

            param = new SqlParameter("@expiry", expiry);
            list.Add(param);

            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"[spSaveAgreement]", list, true);
            }

            catch (Exception ex) { }
            finally { }

            return rvalue;
        }

        public DataTable GetAgreementList()
        {
            try
            {
                dt = new Sqldatalayer().SelectData(null,"[Get_Agreement_Details]", null, true);
            }
            catch (Exception ex) { }
            finally { }

            return dt;
        }

        public DataTable GetAgreementById(int id)
        {

            dt = new DataTable();

            param = new SqlParameter("@Id", id);
            list.Add(param);

            try
            {
                dt = new Sqldatalayer().SelectData(null,"[spGetAgreementById]", list, true);
            }

            catch (Exception ex) { }
            finally { }

            return dt;
        }

        public int EditAgreementById(int id, int secid, string title, string filename, string expiry)
        {
            param = new SqlParameter("@id", id);
            list.Add(param);

            param = new SqlParameter("@secid", secid);
            list.Add(param);

            param = new SqlParameter("@title", title);
            list.Add(param);

            param = new SqlParameter("@filename", filename);
            list.Add(param);

            param = new SqlParameter("@expiry", expiry);
            list.Add(param);


            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"[spEditAgreementById]", list, true);
            }

            catch (Exception ex)
            { }
            finally { }


            return rvalue;
        }

        public int DeleteAgreementById(int id)
        {
            param = new SqlParameter("@Id", id);
            list.Add(param);

            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"[spDeleteAgreementById]", list, true);
            }
            catch (Exception ex)
            { }
            finally { }

            return rvalue;
        }


        // Letter & Circular
        public int SaveCircular(int secid, string title, string filename, string expiry)
        {
            param = new SqlParameter("@secid", secid);
            list.Add(param);

            param = new SqlParameter("@title", title);
            list.Add(param);

            param = new SqlParameter("@filename", filename);
            list.Add(param);

            param = new SqlParameter("@expiry", expiry);
            list.Add(param);

            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"[spSaveCircular]", list, true);
            }

            catch (Exception ex) { }
            finally { }

            return rvalue;
        }

        public DataTable GetCircularList()
        {
            try
            {
                dt = new Sqldatalayer().SelectData(null,"[spGetCircular]", null, true);
            }
            catch (Exception ex) { }
            finally { }

            return dt;
        }

        public DataTable GetCircularById(int id)
        {

            dt = new DataTable();

            param = new SqlParameter("@Id", id);
            list.Add(param);

            try
            {
                dt = new Sqldatalayer().SelectData(null,"[spGetCircularById]", list, true);
            }

            catch (Exception ex) { }
            finally { }

            return dt;
        }

        public int EditCircularById(int id, int secid, string title, string filename, string expiry)
        {
            param = new SqlParameter("@id", id);
            list.Add(param);

            param = new SqlParameter("@secid", secid);
            list.Add(param);

            param = new SqlParameter("@title", title);
            list.Add(param);

            param = new SqlParameter("@filename", filename);
            list.Add(param);

            param = new SqlParameter("@expiry", expiry);
            list.Add(param);


            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"[spEditCircularById]", list, true);
            }

            catch (Exception ex)
            { }
            finally { }


            return rvalue;
        }

        public int DeleteCircularById(int id)
        {
            param = new SqlParameter("@Id", id);
            list.Add(param);

            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"[spDeleteCircularById]", list, true);
            }
            catch (Exception ex)
            { }
            finally { }

            return rvalue;
        }


        // Marquee
        public int SaveMarquee(string title, string link)
        {
            param = new SqlParameter("@title", title);
            list.Add(param);

            param = new SqlParameter("@link", link);
            list.Add(param);


            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"spSaveMarquee", list, true);
            }

            catch (Exception ex)
            { }
            finally { }


            return rvalue;
        }

        public int EditMarqueeById(int id, string title, string link)
        {
            param = new SqlParameter("@id", id);
            list.Add(param);

            param = new SqlParameter("@title", title);
            list.Add(param);

            param = new SqlParameter("@link", link);
            list.Add(param);


            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"spEditMarqueeById", list, true);
            }

            catch (Exception ex)
            { }
            finally { }


            return rvalue;
        }


        public DataTable GetMarqueeList()
        {
            dt = new DataTable();

            try
            {
                dt = new Sqldatalayer().SelectData(null,"spGetMarquee", null, true);
            }
            catch (Exception ex)
            { }
            finally { }

            return dt;
        }

        public DataTable GetMarqueeById(int id)
        {
            SqlParameter param = new SqlParameter();

            param = new SqlParameter("@Id", id);
            list.Add(param);

            try
            {
                dt = new Sqldatalayer().SelectData(null,"[spGetMarqueeById]", list, true);
            }
            catch (Exception ex)
            { }
            finally { }

            return dt;
        }


        public int DeleteMarqueeById(int id)
        {
            param = new SqlParameter("@Id", id);
            list.Add(param);

            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"[spDeleteMarqueeById]", list, true);
            }
            catch (Exception ex)
            { }
            finally { }

            return rvalue;
        }


        // Download
        public int SaveDownload(string title, string filename)
        {
            param = new SqlParameter("@title", title);
            list.Add(param);

            param = new SqlParameter("@filename", filename);
            list.Add(param);

            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"spSaveDownload", list, true);
            }

            catch (Exception ex)
            { }
            finally { }


            return rvalue;
        }

        public int EditDownloadById(int id, string title, string filename)
        {
            param = new SqlParameter("@id", id);
            list.Add(param);

            param = new SqlParameter("@title", title);
            list.Add(param);

            param = new SqlParameter("@filename", filename);
            list.Add(param);

            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"spEditDownloadById", list, true);
            }

            catch (Exception ex)
            { }
            finally { }


            return rvalue;
        }

        public DataTable GetDownloadList()
        {
            dt = new DataTable();

            try
            {
                dt = new Sqldatalayer().SelectData(null,"spGetDownload", null, true);
            }
            catch (Exception ex)
            { }
            finally { }

            return dt;
        }

        public DataTable GetDownloadById(int id)
        {
            SqlParameter param = new SqlParameter();

            param = new SqlParameter("@Id", id);
            list.Add(param);

            try
            {
                dt = new Sqldatalayer().SelectData(null,"[spGetDownloadById]", list, true);
            }
            catch (Exception ex)
            { }
            finally { }

            return dt;
        }

        public int DeleteDownloadById(int id)
        {
            param = new SqlParameter("@Id", id);
            list.Add(param);

            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"[spDeleteDownloadById]", list, true);
            }
            catch (Exception ex)
            { }
            finally { }

            return rvalue;
        }


        // BusinessReport
        public int SaveBusinessReport(string title, string filename)
        {
            param = new SqlParameter("@title", title);
            list.Add(param);

            param = new SqlParameter("@filename", filename);
            list.Add(param);

            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"spSaveBusinessReport", list, true);
            }

            catch (Exception ex)
            { }
            finally { }


            return rvalue;
        }

        public int EditBusinessReportById(int id, string title, string filename)
        {
            param = new SqlParameter("@id", id);
            list.Add(param);

            param = new SqlParameter("@title", title);
            list.Add(param);

            param = new SqlParameter("@filename", filename);
            list.Add(param);

            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"spEditBusinessReportById", list, true);
            }

            catch (Exception ex)
            { }
            finally { }


            return rvalue;
        }

        public DataTable GetBusinessReportList()
        {
            dt = new DataTable();

            try
            {
                dt = new Sqldatalayer().SelectData(null,"spGetBusinessReport", null, true);
            }
            catch (Exception ex)
            { }
            finally { }

            return dt;
        }

        public DataTable GetBusinessReportById(int id)
        {
            SqlParameter param = new SqlParameter();

            param = new SqlParameter("@Id", id);
            list.Add(param);

            try
            {
                dt = new Sqldatalayer().SelectData(null,"[spGetBusinessReportById]", list, true);
            }
            catch (Exception ex)
            { }
            finally { }

            return dt;
        }


        public int DeleteBusinessReportById(int id)
        {
            param = new SqlParameter("@Id", id);
            list.Add(param);

            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"[spDeleteBusinessReportById]", list, true);
            }
            catch (Exception ex)
            { }
            finally { }

            return rvalue;
        }


        // Appointment
        public int SaveAppointment(string title, string filename, string expiry)
        {
            param = new SqlParameter("@title", title);
            list.Add(param);

            param = new SqlParameter("@filename", filename);
            list.Add(param);

            param = new SqlParameter("@expiry", expiry);
            list.Add(param);

            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"[spSaveAppointment]", list, true);
            }

            catch (Exception ex) { }
            finally { }

            return rvalue;
        }

        public DataTable GetAppointmentList()
        {
            try
            {
                dt = new Sqldatalayer().SelectData(null,"[spGetAppointment]", null, true);
            }
            catch (Exception ex) { }
            finally { }

            return dt;
        }

        public DataTable GetAppointmentById(int id)
        {

            dt = new DataTable();

            param = new SqlParameter("@Id", id);
            list.Add(param);

            try
            {
                dt = new Sqldatalayer().SelectData(null,"[spGetAppointmentById]", list, true);
            }

            catch (Exception ex) { }
            finally { }

            return dt;
        }

        public int EditAppointmentById(int id, string title, string filename, string expiry)
        {
            param = new SqlParameter("@id", id);
            list.Add(param);

            param = new SqlParameter("@title", title);
            list.Add(param);

            param = new SqlParameter("@filename", filename);
            list.Add(param);

            param = new SqlParameter("@expiry", expiry);
            list.Add(param);


            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"[spEditAppointmentById]", list, true);
            }

            catch (Exception ex)
            { }
            finally { }


            return rvalue;
        }

        public int DeleteAppointmentById(int id)
        {
            param = new SqlParameter("@Id", id);
            list.Add(param);

            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"[spDeleteAppointmentById]", list, true);
            }
            catch (Exception ex)
            { }
            finally { }

            return rvalue;
        }



        // Recruitment
        public int SaveRecruitment(string title, string filename, string expiry)
        {
            param = new SqlParameter("@title", title);
            list.Add(param);

            param = new SqlParameter("@filename", filename);
            list.Add(param);

            param = new SqlParameter("@expiry", expiry);
            list.Add(param);

            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"[spSaveRecruitment]", list, true);
            }

            catch (Exception ex) { }
            finally { }

            return rvalue;
        }

        public DataTable GetRecruitmentList()
        {
            try
            {
                dt = new Sqldatalayer().SelectData(null,"[spGetRecruitment]", null, true);
            }
            catch (Exception ex) { }
            finally { }

            return dt;
        }

        public DataTable GetRecruitmentById(int id)
        {

            dt = new DataTable();

            param = new SqlParameter("@Id", id);
            list.Add(param);

            try
            {
                dt = new Sqldatalayer().SelectData(null,"[spGetRecruitmentById]", list, true);
            }

            catch (Exception ex) { }
            finally { }

            return dt;
        }

        public int EditRecruitmentById(int id, string title, string filename, string expiry)
        {
            param = new SqlParameter("@id", id);
            list.Add(param);

            param = new SqlParameter("@title", title);
            list.Add(param);

            param = new SqlParameter("@filename", filename);
            list.Add(param);

            param = new SqlParameter("@expiry", expiry);
            list.Add(param);


            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"[spEditRecruitmentById]", list, true);
            }

            catch (Exception ex)
            { }
            finally { }


            return rvalue;
        }

        public int DeleteRecruitmentById(int id)
        {
            param = new SqlParameter("@Id", id);
            list.Add(param);

            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"[spDeleteRecruitmentById]", list, true);
            }
            catch (Exception ex)
            { }
            finally { }

            return rvalue;
        }



        //Current Gallery
        public int SaveGallery(string title, string desc, string imagename)
        {
            param = new SqlParameter("@title", title);
            list.Add(param);

            param = new SqlParameter("@desc", desc);
            list.Add(param);

            param = new SqlParameter("@imagename", imagename);
            list.Add(param);

            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"spSaveGallery", list, true);
            }

            catch (Exception ex)
            { }
            finally { }


            return rvalue;
        }

        public int EditGalleryById(int id, string title, string desc, string imagename)
        {
            param = new SqlParameter("@id", id);
            list.Add(param);

            param = new SqlParameter("@title", title);
            list.Add(param);

            param = new SqlParameter("@desc", desc);
            list.Add(param);

            param = new SqlParameter("@imagename", imagename);
            list.Add(param);

            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"spEditGalleryById", list, true);
            }

            catch (Exception ex)
            { }
            finally { }


            return rvalue;
        }


        public DataTable GetGalleryList()
        {
            dt = new DataTable();

            try
            {
                dt = new Sqldatalayer().SelectData(null,"spGetGalleryList", null, true);
            }
            catch (Exception ex)
            { }
            finally { }

            return dt;
        }

        public DataTable GetGalleryById(int id)
        {
            SqlParameter param = new SqlParameter();

            param = new SqlParameter("@Id", id);
            list.Add(param);

            try
            {
                dt = new Sqldatalayer().SelectData(null,"[spGetGalleryById]", list, true);
            }
            catch (Exception ex)
            { }
            finally { }

            return dt;
        }


        public int DeleteGalleryById(int id)
        {
            param = new SqlParameter("@Id", id);
            list.Add(param);

            try
            {
                rvalue = new Sqldatalayer().ExecuteNonQuery(null,"[spDeleteGalleryById]", list, true);
            }
            catch (Exception ex)
            { }
            finally { }

            return rvalue;
        }

        public DataTable CreateNewUser(String Xml)
        {
            try
            {
                strSql = "exec Create_New_User  @strXML='" + Xml.Trim().Replace("'", "") + "'";
                return SqlDataAccess.ExecuteDataset(SqlDataAccess.ConnectionString, CommandType.Text, strSql).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetCreateUserbyid(Int32 UserID)
        {

            try
            {
                strSql = "exec Get_Crete_Users_By_ID " + UserID + "";
                return SqlDataAccess.ExecuteDataset(SqlDataAccess.ConnectionString, CommandType.Text, strSql).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetCreteUsers()
        {

            try
            {
                strSql = "exec Get_Crete_Users ";
                return SqlDataAccess.ExecuteDataset(SqlDataAccess.ConnectionString, CommandType.Text, strSql).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        // HRMIS Function

        public DataTable InsertEmployeeDetails(String Xml)
        {
            try
            {
                strSql = "exec HRMIS_Employee_Details_Insert  @strXML='" + Xml.Trim().Replace("'", "") + "'";
                return SqlDataAccess.ExecuteDataset(SqlDataAccess.ConnectionString, CommandType.Text, strSql).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable HRMIS_Employee_Posting_Details_Insert(String Xml)
        {
            try
            {
                strSql = "exec HRMIS_Employee_Posting_Details_Insert  @strXML='" + Xml.Trim().Replace("'", "") + "'";
                return SqlDataAccess.ExecuteDataset(SqlDataAccess.ConnectionString, CommandType.Text, strSql).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable HRMIS_Get_Employee_Detials(Int32 EmpID)
        {

            try
            {
                strSql = "exec HRMIS_Get_Employee_Detials " + EmpID + "";
                return SqlDataAccess.ExecuteDataset(SqlDataAccess.ConnectionString, CommandType.Text, strSql).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable HRMIS_Get_Employee_Posting_Details(Int32 EmpID)
        {

            try
            {
                strSql = "exec HRMIS_Get_Employee_Posting_Details " + EmpID + "";
                return SqlDataAccess.ExecuteDataset(SqlDataAccess.ConnectionString, CommandType.Text, strSql).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable HRMIS_Get_Employee_Posting_Details_For_Edit(Int32 ID)
        {

            try
            {
                strSql = "exec HRMIS_Get_Employee_Posting_Details_For_Edit " + ID + "";
                return SqlDataAccess.ExecuteDataset(SqlDataAccess.ConnectionString, CommandType.Text, strSql).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
