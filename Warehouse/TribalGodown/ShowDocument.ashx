<%@ WebHandler Language="C#" Class="ShowDocument" %>

using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class ShowDocument : IHttpHandler {

    public SqlConnection connStr = new SqlConnection(ConfigurationManager.ConnectionStrings["TribalGodownConString"].ToString());
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.QueryString["TAID"] == null) return;

        string pictureId = context.Request.QueryString["TAID"];
        using (SqlConnection conn = connStr)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT [RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic] From [Tbl_TribalReg] where TAID=@TAID", conn))
            {
                cmd.Parameters.Add(new SqlParameter("@TAID", pictureId));
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    reader.Read();
                    
                    context.Response.BinaryWrite((Byte[])reader[reader.GetOrdinal("RojgarPic")]);
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