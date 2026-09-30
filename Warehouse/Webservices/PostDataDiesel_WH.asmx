<%@ WebService Language="C#" Class="PostDataDiesel_WH" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using System.Security.Principal;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.IO;
using System.Web;


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class PostDataDiesel_WH : System.Web.Services.WebService
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_Kharif2023"].ToString());
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MPSCSCConnectionString"].ToString());
    //public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_Kharif2023"].ToString());
    //SendSms_ACL_WebServer sendSMS = new SendSms_ACL_WebServer();
    //SendSms_Webservice_withTemplate.SendSms_MicroService smsObj = new SendSms_Webservice_withTemplate.SendSms_MicroService();
    [WebMethod]

    public string PostDataDieselData_WH(string UserName, string PassKey,string Dist_Id, string Procurement_Year, string IP, string DieselRate)
    {
        string strMsg = "";
        if (UserName == "eup2023_WebApp" && PassKey == "$#KHeup2023")
        {
            SqlCommand cmd = new SqlCommand();
            string str = "";
            try
            {
                Dist_Id = "23" + Dist_Id;

                str = "INSERT INTO dbo.DieselRate_Master (Dist_Id,Procurement_Year,DieselRate,IP) values('" + Dist_Id + "','" + Procurement_Year + "','" + DieselRate + "','" + IP + "')";

                cmd = new SqlCommand(str, con);
                //   cmd.CommandTimeout = 0;
                if (con.State == ConnectionState.Closed) { con.Open(); }
                int i = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open) { con.Close(); }
                strMsg = "Save Record";
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                strMsg = ex.Message;
                //SendExcepToDB(ex, str, IP);
            }
            finally
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
            }
        }
        else
        {
            strMsg = "Invalid Password";
        }
        return strMsg;
    }


}