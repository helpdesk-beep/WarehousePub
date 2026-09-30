<%@ WebHandler Language="C#" Class="ViewAdvertiesment" %>

using System.Web;
using System.Data.SqlClient;
using System.Configuration;

public class ViewAdvertiesment : IHttpHandler {
    
   public void ProcessRequest(HttpContext context)
    {
        int id = int.Parse(context.Request.QueryString["Id"]);
        byte[] bytes;
        string fileName, contentType;
        string constr = ConfigurationManager.ConnectionStrings["RajCon"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select_advertiesment_by_id",con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["fileDataHn"];
                    contentType = sdr["contentTypeHn"].ToString();
                    fileName = sdr["fileNameHn"].ToString();
                }
                con.Close();
            }
        }
 
        context.Response.Buffer = true;
        context.Response.Charset = "";
        if (context.Request.QueryString["download"] == "1")
        {
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        }
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        context.Response.ContentType = "application/pdf";
        context.Response.BinaryWrite(bytes);
        context.Response.Flush();
        context.Response.End();
    }
 
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}