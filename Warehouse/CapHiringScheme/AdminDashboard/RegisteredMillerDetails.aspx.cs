using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public partial class CapHiringScheme_AdminDashboard_RegisteredMillersDetails : System.Web.UI.Page
{
    DataTable dt=new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Mill_Id"].ToString() != null)
        {
           string millid= Request.QueryString["Mill_Id"].ToString();
           
            dt=GetMillRegistrationMaster(millid);
            rptRegMillerDetails.DataSource = dt;
            rptRegMillerDetails.DataBind();
        }

    }


    public DataTable GetMillRegistrationMaster(string millid)
    {

        string qr = "select mreg.Mill_Id,mreg.CreateOn, mreg.Mill_Name, mreg.Registration_ID, pr.Miller, pr.Mobile, pr.Email, pr.Aadhar, pr.Pan, mreg.Dist_Ind_RegNo, mreg.RegNo_Issue_Date,mreg.Mpscsc_Markfed, mreg.Agree_Miller_Id,mreg.Agree_Date, mreg.Agree_Capacity, mreg.Paddy_Capacity, mreg.Rice_Capacity, mreg.Office_Contact, mreg.Mill_Landmark, st.State_Name, ds.District_Name, th.Tehsil_Name, bl.Block_Name, br.BranchName, mreg.Near_Distance, mreg.Mill_Office_Address, mreg.Incharge_Peson, mreg.Incharge_Post, mreg.Incharge_Email, mreg.Incharge_Mobile, mreg.Incharge_Address, mreg.Mill_Lat, mreg.Mill_Long, mreg.CreateOn from dbo.MillRegistrationMaster as mreg inner join PreRegistration as pr on pr.Registration_ID=mreg.Registration_ID inner join dbo.tbl_Metadata_State as st on st.State_Code=mreg.State_Id inner join dbo.tbl_MetaData_DISTRICT as ds on ds.District_Id=mreg.District_Id inner join dbo.Tehsils as th on th.TehsilCode=mreg.Tehsil_Code inner join dbo.tbl_Branch_Block_Mapping as bl on bl.Block_ID=mreg.Block_Id inner join dbo.MetaDataBranchWithIssueCenter as br on br.BranchID=mreg.Near_Branch_Id where mreg.Mill_Id='" + millid + "'";

        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);
        }

        catch (Exception ex) { }
        finally { }
        return dt;
    }


    protected void lnkDownloadFile_OnClick(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

        HiddenField hdnMillId = (item.FindControl("hdnMillId") as HiddenField);


        int millid = int.Parse(hdnMillId.Value);
        byte[] bytes;
        string fileName, contentType;
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select Doc_File_Name,Doc_File_Type,Doc_File from MillRegistrationMaster where Mill_Id=@Id";
                cmd.Parameters.AddWithValue("@Id", millid);
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Doc_File"];
                    contentType = sdr["Doc_File_Type"].ToString();
                    fileName = sdr["Doc_File_Name"].ToString() + contentType;
                }
                con.Close();
            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();




    }
}