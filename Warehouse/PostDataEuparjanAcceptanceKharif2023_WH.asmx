<%@ WebService Language="C#" Class="PostDataEuparjanAcceptanceKharif2023_WH" %>

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
public class PostDataEuparjanAcceptanceKharif2023_WH : System.Web.Services.WebService
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["MPSCSCConnectionString"].ToString());

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MPSCSCConnectionString"].ToString());
    //public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_Kharif2023"].ToString());
    //SendSms_ACL_WebServer sendSMS = new SendSms_ACL_WebServer();
    //SendSms_Webservice_withTemplate.SendSms_MicroService smsObj = new SendSms_Webservice_withTemplate.SendSms_MicroService();
    [WebMethod]

    public string PostAcceptanceDataKharif2023_WH(string UserName, string PassKey, string comm, string State, string Distt, string IC_ID, string SendDist, string P_C, string Dispatch_Dt, string TC_No, string Truck_No, string TransptrID, string crp_Id, string CrpYear, string No_Bags, string Quanity, string Accpt_No, string Accpt_Date, string Bk_No, string RecdBgs, string RcdQty, string Rcd_Dt, string Rcd_Gdwn, string RecptId, string Month, string Year, string Stts_Dpsit, string Up_Date, string Del_Date, string IP, string ANsttus, string OprID, string Branch_Id, string RecdQty_Faq, string RecdBags_JuteNew, string RcdBgsP, string RcdBgs_JtOld, string Stchngbgs, string StencileBg, string Moisture, string TulParc, string category, string GTypeId, string Transp_Pancard, string WeighbrdgeID, string WeighbrdgTulPrci, string Weighbridge_LoadedQty, string Weighbridge_EmptyQty, string StackName, string StackNumber, string BagsWeight_PP, string BagsWeight_JuteNew, string BagsWeight_JuteOld, string GrossWeight, string NetWeight, string Bags_Nottagged, string Bags_NotColorCode, string DepositerNo, string Reject_Bags, string Rejected_NetWeight, string ParisarTyp, string IsRejected, string Comodity_type, string slogin_name, string slogin_mob, string slogin_des,string tempcode, string IsPartial, string Season)
    {
        string strMsg = "";
        if (UserName == "eup2023_WebApp" && PassKey == "$#KHeup2023")
        {
            SqlCommand cmd = new SqlCommand();
            string str = "";
            try
            {
                Distt = "23" + Distt;
                SendDist = "23" + SendDist;

                if (crp_Id == "8") { crp_Id = "6"; }
                else if (crp_Id == "11") { crp_Id = "4"; }
                else if (crp_Id == "13") { crp_Id = "2"; }
                else if (crp_Id == "14") { crp_Id = "3"; }

                if (IsRejected == "N" && Bk_No == "0")
                {
                    str = "INSERT INTO MPSCSC.dbo.Acceptance_Note_Kharif2023 (State_Id,Distt_ID,IssueCenter_ID,Sending_District,Purchase_Center,Dispatch_Date, TC_Number,Truck_Number, Transporter_ID,Commodity_Id,Crop_Year,No_of_Bags,Quantity,Acceptance_No,Acceptance_Date,Book_No,Recd_Bags,Recd_Qty,Recd_Date,Recd_Godown, Receipt_Id,Month,Year,Status_Deposit,Created_Date,Updates_Date,Deleted_Date,IP_Address,AN_status,OperatorID,Branch_Id, RecdQty_Faq ,RecdBags_JuteNew ,RecdBags_PP,RecdBags_JuteOld,Stiching_bags ,Stencile_bags,Moisture,TaulParchi,category,GodownTypeId,Transp_Pancard,Weighbridge_ID,Weighbridge_TaulParchi, Weighbridge_LoadedQty,Weighbridge_EmptyQty ,StackName ,StackNumber  ,BagsWeight_PP  ,BagsWeight_JuteNew   ,BagsWeight_JuteOld ,GrossWeight  ,AcceptanceQty  ,Bags_Nottagged  ,Bags_NotColorCode,  DepositerNo,Reject_Bags,RejectedQty,ParisarTyp,IsRejected,InsertFrom,Comodity_Type, tempcode,slogin_name,slogin_mob,slogin_des,IsPartial,Season) values('" + State + "','" + Distt + "','" + IC_ID + "','" + SendDist + "','" + P_C + "','" + Dispatch_Dt + "','" + TC_No + "','" + Truck_No + "','" + TransptrID + "','" + crp_Id + "','" + CrpYear + "'," + No_Bags + "," + Quanity + ",'" + Accpt_No + "','" + Accpt_Date + "','" + Bk_No + "'," + RecdBgs + "," + RcdQty + ",'" + Rcd_Dt + "','" + Rcd_Gdwn + "','" + RecptId + "'," + Month + "," + Year + ",'" + Stts_Dpsit + "',getdate(),'" + Up_Date + "','" + Del_Date + "','" + IP + "','" + ANsttus + "','" + OprID + "','" + Branch_Id + "'," + RecdQty_Faq + "," + RecdBags_JuteNew + "," + RcdBgsP + "," + RcdBgs_JtOld + "," + Stchngbgs + "," + StencileBg + ",'','" + TulParc + "','" + category + "'," + GTypeId + ",'" + Transp_Pancard + "','" + WeighbrdgeID + "','" + WeighbrdgTulPrci + "','" + Weighbridge_LoadedQty + "','" + Weighbridge_EmptyQty + "','',''," + BagsWeight_PP + "," + BagsWeight_JuteNew + "," + BagsWeight_JuteOld + "," + GrossWeight + "," + NetWeight + "," + Bags_Nottagged + "," + Bags_NotColorCode + ",'" + DepositerNo + "'," + Reject_Bags + "," + Rejected_NetWeight + ",'" + ParisarTyp + "','" + IsRejected + "','WEB','" + Comodity_type + "','" + tempcode + "','" + slogin_name + "','" + slogin_mob + "','" + slogin_des + "','" + IsPartial + "','" + Season + "')";
                }


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