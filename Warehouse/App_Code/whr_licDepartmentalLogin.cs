using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DepartmentalLogin
/// </summary>
public class whr_licDepartmentalLogin
{

    SqlConnection con = new SqlConnection(whr_licConnection.con());
   SqlCommand cmd = null;

    //SqlTransaction trans = new SqlTransaction();
    DataSet ds = new DataSet();
    SqlDataAdapter da = null;

    int i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        con.Open();

    }
    public int Login_Departmental(string post, string username, string pwd, string ip)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlTransaction trans = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

        try
        {
            cmd = new SqlCommand("Select * from tbl_Department_Login where UserPost=@UserPost and Dept_Username=@Username and Dept_Pwd=@Password", con, trans);
            cmd.Parameters.AddWithValue("@UserPost", post);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", pwd);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                cmd = new SqlCommand("update tbl_Department_Login set Ip=@Ip,Login_Date=Getdate() where UserPost=@UserPost and Dept_Username=@Username and Dept_Pwd=@Password", con, trans);
                cmd.Parameters.AddWithValue("@Ip", ip);
                cmd.Parameters.AddWithValue("@UserPost", post);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", pwd);
                cmd.ExecuteNonQuery();
                trans.Commit();
                i = 1;
            }
            else
            {
                trans.Rollback();
            }

        }
        catch (Exception ex)
        {
            trans.Rollback();
            i = 0;
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);

        }
        return i;
    }




    public DataSet Login_User(string username, string pwd, string ip)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlTransaction trans = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

        try
        {
            cmd = new SqlCommand("SELECT * FROM [wh_license].[dbo].[tbl_ApplicantRegistration] where UserId=@Username and Pwd=@Password", con, trans);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", pwd);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string id = ds.Tables[0].Rows[0]["ApplicantID"].ToString();
                cmd = new SqlCommand("update [tbl_ApplicantRegistration] set IP=@Ip,LoginDate=GETDATE() where ApplicantID=@ApplicantID and Userid=@Username and Pwd=@Password", con, trans);
                cmd.Parameters.AddWithValue("@Ip", ip);
                cmd.Parameters.AddWithValue("@ApplicantID", id);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", pwd);
                cmd.ExecuteNonQuery();
                trans.Commit();
                // i = 1;

            }
            else
            {
                trans.Rollback();
            }
        }
        catch (Exception ex)
        {
            trans.Rollback();
            i = 0;
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);

        }
        return ds;
    }

    public string Reg_Applicant(string appUsername, string frmname, string mobileno, string emailid, string userid, string pwd, string confpwd, string ip, string UsertypeId)
    {
        int i = 0;
        string regid = "";
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlTransaction trans = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);



        try
        {
            cmd = new SqlCommand("sp_applicantreg", con, trans);
            // cmd.CommandText = "sp_applicantreg";
            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Applicantname", appUsername);
            cmd.Parameters.Add("@ApplicantFirmname", frmname);
            cmd.Parameters.Add("@Mobileno", mobileno);
            cmd.Parameters.Add("@EmailId", emailid);
            cmd.Parameters.Add("@UserId", userid);
            cmd.Parameters.Add("@Pwd", pwd);
            cmd.Parameters.Add("@Confpwd", confpwd);
            cmd.Parameters.Add("@IP", ip);
            cmd.Parameters.Add("@UsertypeId", UsertypeId);

          
            cmd.Parameters.Add("@outid", SqlDbType.VarChar, 50);
            cmd.Parameters["@outid"].Direction = ParameterDirection.Output;

            //@outid
            cmd.ExecuteNonQuery();

            regid = Convert.ToString(cmd.Parameters["@outid"].Value);
            trans.Commit();

        }
        catch (Exception ex)
        {
            trans.Rollback();
            i = 0;
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);

        }
        return regid;
    }

    public int DocumentUpload(string id, string ip, string gdcertificate, string lience, string ghoshna, string insurance, string sampler)
    {
        SqlCommand cmd = new SqlCommand("sp_Document_Replace", con);
        cmd.CommandTimeout = 0;
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add("@WhrId", id);
        cmd.Parameters.Add("@ip", ip);
        cmd.Parameters.Add("@GdCertificate_pdf", gdcertificate);
        cmd.Parameters.Add("@liencesFeeChallan_Pdf", lience);
        cmd.Parameters.Add("@GhoshnaCertificate_pdf", ghoshna);
        cmd.Parameters.Add("@InsurancePolicy_pdf", insurance);
        cmd.Parameters.Add("@SamplerLience_Pdf", sampler);
        int i = cmd.ExecuteNonQuery();

        return i;

    }


}
