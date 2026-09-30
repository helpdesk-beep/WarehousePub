using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class E_WHR_Delete_Digital_SignfromWHR : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() == "MPSWLC")
        {
            try
            {
                if (!IsPostBack)
                {
                    fillDistrict();
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void fillDistrict()
    {
        try
        {
            string query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, "---Select---");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDSCDetail();
    }
   
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/E_WHR/Delete_Digital_SignfromWHR.aspx");
    }
    private void GetDSCDetail()
    {
        try
        {
            string DistrictId = ddlDistrict.SelectedValue.ToString();
            string WHR_No = txtSearchWHR.Text;
            //string qry = "select Aid, (select Depotname from tbl_metadata_depot as MD Where MD.BranchID=DSC.Branch_ID) as Branch,BG_Name as Branch_Godown_Name,SUBSTRING(SUBSTRING(SubjectName,0,CHARINDEX(',',SubjectName)), 4, Len(SubjectName)) as DSC_HOLDER_NAME,SerialNumber,SUBSTRING(SUBSTRING(IssuerName,0,CHARINDEX(',',IssuerName)), 4, Len (IssuerName)) as DSC_IssuerName,convert(varchar(10),NotAfter,103) as ValidUpto,convert(varchar(10),Created_Date,103) as DSC_UploadDate,case when User_Type='G' then 'Godown'  when User_Type='B' then 'Branch'  end User_Type,Verification_Status from tbl_DSC_User_Upload_Detail as DSC where DSC.District_Id='" + DistrictId + "' Order by Branch,BG_Name";
            //string qry = "select GD.Godown_Name as Godown,GD.Godown_ID,DSWHR.Depositor_WHR_Id as WHR_Id,DSWHR.DSC_Holder_Name as Signing_Person,DSWHR.DSC_Serial_No as Serial_No,DSWHR.CreatedDate as Signing_Date,DSWHR.Client_Ip as Signing_Ip ,DSWHR.TotalBags_Received as Bags,DSWHR.Total_Qty_Received as Qty,CONVERT(varchar(10),DSWHR.Date_of_Deposit,103) as Date_of_Deposit,DSWHR.DSC_User_Type as User_Type,Commodity_Id from tbl_Digitally_Signed_WHR_Details as DSWHR inner join tbl_Digital_Signature_Details as DSign on DSign.WHR_No=DSWHR.Depositor_WHR_Id and DSign.DSC_Serial_No=DSWHR.DSC_Serial_No inner join tbl_DSC_WHR_XML_File as DXML on DXML.WHR_Id=DSWHR.Depositor_WHR_Id and DXML.DSC_Serial_No=DSWHR.DSC_Serial_No inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID=DSWHR.GodownID where DSWHR.District_Id='" + DistrictId + "' and DSWHR.Depositor_WHR_Id not in (select distinct A.WHR_Id from tbl_DSC_eWHR_Submission as A where A.WHR_Id='" + WHR_No + "') and DSWHR.Depositor_WHR_Id='" + WHR_No + "'";
            string qry = "select GD.Godown_Name as Godown,GD.Godown_ID,DSWHR.Depositor_WHR_Id as WHR_Id,DSWHR.DSC_Holder_Name as Signing_Person,DSWHR.DSC_Serial_No as Serial_No,DSWHR.CreatedDate as Signing_Date,DSWHR.Client_Ip as Signing_Ip ,DSWHR.TotalBags_Received as Bags,DSWHR.Total_Qty_Received as Qty,CONVERT(varchar(10),DSWHR.Date_of_Deposit,103) as Date_of_Deposit,DSWHR.DSC_User_Type as User_Type,Commodity_Id from tbl_Digitally_Signed_WHR_Details as DSWHR inner join tbl_Digital_Signature_Details as DSign on DSign.WHR_No=DSWHR.Depositor_WHR_Id and DSign.DSC_Serial_No=DSWHR.DSC_Serial_No inner join tbl_DSC_WHR_XML_File as DXML on DXML.WHR_Id=DSWHR.Depositor_WHR_Id and DXML.DSC_Serial_No=DSWHR.DSC_Serial_No inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID=DSWHR.GodownID where DSWHR.District_Id='" + DistrictId + "' and DSWHR.Depositor_WHR_Id not in (select distinct A.WHR_Id from tbl_DSC_eWHR_Submission as A where A.WHR_Id='" + WHR_No + "') and DSWHR.Depositor_WHR_Id='" + WHR_No + "'  and DSWHR.Depositor_WHR_Id in (select WHRID from whrprintstatus where WHRID='" + WHR_No + "' and PrintStatus='1st')";

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //lblRowCount.Text = "Total records are : " + ds.Tables[0].Rows.Count.ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                //godown_GridView.DataSource = ds.Tables[0];
                //godown_GridView.DataBind();
                Session["dsGodown"] = ds;
                fillGrid(ds);

            }
            else
            {
                Session["dsGodown"] = null;
                fillGrid(ds);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not Found')", true);
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    private void fillGrid(DataSet ds)
    {
        godown_GridView.DataSource = ds.Tables[0];
        godown_GridView.DataBind();
        // lblRowCount.Text = "Total records are : " + godown_GridView.Rows.Count.ToString();
    }
    protected void godown_GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string District_Id = ddlDistrict.SelectedValue.ToString();
            int _rowindex = e.RowIndex;
            string WHR_ID = txtSearchWHR.Text;
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            //string log_qry = "insert into tbl_DSC_User_Upload_Detail_Log SELECT [Aid],[District_Id],[Branch_Id],[DSC_ID],[DSC_Holder_Name],[Mobile_No],[BG_Name],[BG_ID],[User_Type],[SerialNumber],[SubjectName],[IssuerName],[publicKey],[FriendlyName],[CertificateVerified],[SimpleName],[SignatureAlgorithm],[CertificateArchived],[Thumbprint],[NotBefore],[NotAfter],[CertificateStr],[CertificateXmlPK],[RawDataLenght],[Version],[Created_Date],[Created_By],[Verification_Status],[Verification_Date],[Verified_By],GETDATE(),'" + ip + "',Client_Ip FROM [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_User_Upload_Detail] where Aid='" + gid + "' and District_Id='" + District_Id + "'";
            string log_qry = "insert into tbl_Digitally_Signed_WHR_Details_Log SELECT [Depositor_WHR_Id] ,[Depositor_Form_No] ,[District_Id] ,[Commodity_Id] ,[Category_Id] ,[Depositor_Name] ,[Date_of_Deposit] ,[TotalBags_Received] ,[Total_Qty_Received] ,[AvgMoisture_Content] ,[AvgMoisture_Content_To] ,[SangrahadDate] ,[MktValue_of_Commodity] ,[WHR_Issue_Date] ,[WHR_CreatedDate] ,[WHR_Client_IP] ,[CropYear] ,[Remark] ,[BranchID] ,[DepositorID] ,[GodownID] ,[WHR_Check_Sum] ,[CreatedDate] ,[CreatedBy] ,[DSC_Serial_No] ,[DSC_Holder_Name] ,[Client_Ip] ,[DSC_User_Type] ,'" + ip + "',GETDATE() FROM [tbl_Digitally_Signed_WHR_Details] where Depositor_WHR_Id='" + WHR_ID + "'";
            cmd = new SqlCommand(log_qry, con);
            con.Open();
            int s = cmd.ExecuteNonQuery();
            con.Close();
            /////////////////////////////////////////
            if (s > 0)
            {
                con.Open();
                //qry = "Delete from Pvt_Warehouse_Login where Login_Id='" + gid + "'";
                qry = "delete from tbl_Digitally_Signed_WHR_Details where Depositor_WHR_Id='"+ WHR_ID +"' and District_Id='"+ District_Id +"'";
                cmd = new SqlCommand(qry, con);
                int d = cmd.ExecuteNonQuery();
                con.Close();
                if (d > 0)
                {
                    string log_qry2 = "insert into tbl_Digital_Signature_Details_Log SELECT [Aid] ,[WHR_No] ,[DSC_Serial_No] ,[DSC_Holder_Name] ,[DSC_Subject] ,[Sig_SignatureValue] ,[Cano_Algorithm] ,[SM_Algorithm] ,[Ref_DigestValue] ,[TF_Algorithm] ,[DM_Algorithm] ,[KeyInfo_KeyName] ,[RSA_Modulus] ,[RSA_Exponent] ,[X509Certificate] ,[DSC_User_Type] ,[CreatedDate] ,[CreatedBy] ,[Client_Ip] ,'" + ip + "' ,GETDATE() FROM [tbl_Digital_Signature_Details] where WHR_No='" + WHR_ID + "'";
                    cmd = new SqlCommand(log_qry2, con);
                    con.Open();
                    int s2 = cmd.ExecuteNonQuery();
                    con.Close();
                    if (s2 > 0)
                    {
                           con.Open();
                          string qry2 = "delete from tbl_Digital_Signature_Details where WHR_No='" + WHR_ID + "'";
                          cmd = new SqlCommand(qry2, con);
                          int d2 = cmd.ExecuteNonQuery();
                          con.Close();
                          if (d2 > 0)
                          {
                              
                              string log_qry3 = "insert into tbl_DSC_WHR_XML_File_Log SELECT [AId] ,[WHR_Id] ,[WHR_XML_File] ,[DSC_Serial_No] ,[DSC_User_Type] ,[CreatedDate] ,[CreatedBy] ,[Client_Ip] ,'" + ip + "' ,GETDATE() FROM [tbl_DSC_WHR_XML_File] where WHR_Id='" + WHR_ID + "'";
                              cmd = new SqlCommand(log_qry3, con);
                              con.Open();
                              int s3 = cmd.ExecuteNonQuery();
                              con.Close();
                              if (s3 > 0)
                              {
                                  string qry3 = "delete from tbl_DSC_WHR_XML_File where WHR_Id='" + WHR_ID + "'";
                                  cmd = new SqlCommand(qry3, con);
                                  con.Open();
                                  int d3 = cmd.ExecuteNonQuery();
                                  con.Close();
                                  if (d3 > 0)
                                  {
                                      ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Deleted Successfully')", true);
                                      GetDSCDetail();
                                  }
                              }
                          }
                    }
                   
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Some error has occured')", true);
                }
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message.ToString();
        }
        finally
        {
            con.Close();
        }
    }
    protected void btnSerachWHR_Click(object sender, EventArgs e)
    {
        GetDSCDetail();
    }
}
