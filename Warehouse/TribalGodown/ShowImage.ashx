<%@ WebHandler Language="C#" Class="ShowImage" %>

using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

    public class ShowImage : IHttpHandler {
    public SqlConnection connStr = new SqlConnection(ConfigurationManager.ConnectionStrings["TribalGodownConString"].ToString());
    public string Qry = "";
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.QueryString["TAID"] == null) return;
     
        string pictureId = context.Request.QueryString["TAID"];
        using (SqlConnection conn = connStr)
        {
            string TrimTAID = pictureId.Substring(2,4);
            if (TrimTAID == "2017")
            {
                Qry = "SELECT [RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic] From [Tbl_TribalReg] where TAID=@TAID";
            }
            else if (TrimTAID == "2019")
            {
                Qry = "SELECT [RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic] From [Tbl_TribalReg] where TAID=@TAID";
            }
            else if (TrimTAID == "2016")
            {
                Qry = "SELECT [RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic] From [Tbl_TribalReg2016] where TAID=@TAID";
            }
            
            using (SqlCommand cmd = new SqlCommand(Qry, conn))
            {
                cmd.Parameters.Add(new SqlParameter("@TAID", pictureId));
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    reader.Read();

                    context.Response.BinaryWrite((Byte[])reader[reader.GetOrdinal("AppPic")]);
                    //context.Response.BinaryWrite((Byte[])reader[reader.GetOrdinal("RojgarPic")]);
                    reader.Close();
                }
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }

}