using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Principal;

public partial class Region_DSCUser_Verification_RO : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    DataSet ds2 = null;
    decimal ChargeOfTotal = 0;
    decimal RebateAmount = 0;
    decimal NetAmount = 0;
    decimal AccruedNetAmount = 0;
    string NetAmountWord = "";
    string Bill_No = "";
    decimal Discount = 0;
    decimal Service_Tax = 0;
    string Bill_Type = "";
    int BID = 0;
    public string GenerateOTP = "", OTPSMS = "";
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() == "MPSWLC" || Session["Region_ID"].ToString() != null)
        {
            if (!IsPostBack)
            {
                GetDSCDetail();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void User_Ver(string Ver_Type,string DSCID,string Serial_No)
    {
        if ((Session["Region_ID"] != null))
        {
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            //string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
            string Region_Id = Session["Region_ID"].ToString();
            SqlTransaction sqltran = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqltran = con.BeginTransaction();
                string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
                //string str = "update tbl_Storage_Bill_Details set BO_Approval_Status='Y',BO_Approval_Date=getdate(),BO_Approval_IP='" + ClientIP + "' where Bill_Number='" + gvBOBillApp.SelectedRow.Cells[1].Text.ToString() + "' and Branch_Id='" + Session["BranchId"].ToString() + "' and District_Id='" + Dist_id + "'";
                string str = "update tbl_DSC_User_Upload_Detail set Verification_Status='" + Ver_Type + "',Verification_Date=GETDATE(),Verified_By='" + ip + "' where Aid='" + DSCID + "' and SerialNumber='" + Serial_No + "'";

                cmd = new SqlCommand(str, con, sqltran);
                int req = cmd.ExecuteNonQuery();
                if (req > 0)
                {
                    sqltran.Commit();
                    con.Close();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('User Successfully Verified....')", true);

                    GetDSCDetail();
                }
                else
                {
                    //lbl_message.Text = "WHR record saved successfully";
                }
            }
            catch (Exception ex)
            {
                sqltran.Rollback();
                Response.Write(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }
    }
    private void  GetDSCDetail()
    {
        try
        {
            string Region_Id = Session["Region_ID"].ToString();
            //string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
            //string str = "select SB.Bill_Number,(select D.Depositor_Name from tbl_MetaData_DEPOSITOR as D where D.Depositor_ID=SB.Depositor_Id) as Depositor_Name,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=SB.Commodity_Id) as Commodity,CONVERT(varchar(10),SB.Created_Date,103) as Billing_Date,SB.Net_Amount,case when SB.Bill_Type='AU' then '' else CONVERT(varchar(10),SB.[From_Date],103) end as [From_Date],case when SB.Bill_Type='AU' then '' else CONVERT(varchar(10),SB.[To_Date],103) end as [To_Date],case when SB.Bill_Type='AD' then 'Daily Storage Charges Bill' when SB.Bill_Type='AU' then 'Accrued Storage Charges Bill' when SB.Bill_Type='OD' then 'Daily Over & Above Storage Charges Bill' when SB.Bill_Type='OU' then 'Accrued Over & Above Storage Charges Bill' when SB.Bill_Type='HG' then 'Godown(Hired) Rent Bill' when SB.Bill_Type='RB' then 'Reservation Bill' when SB.Bill_Type='GR' then 'Godown(JVS) Rent Bill' else '' end as Bill_Name from tbl_Storage_Bill_Details as SB where SB.Created_Date>=CONVERT(varchar(10),'11/01/2016',101) and SB.Branch_Id='" + Session["BranchId"].ToString() + "' and SB.District_Id='" + Dist_id + "' and BO_Approval_Status is null order by SB.Created_Date";
            string str = "select  Aid as DSC_Id,MD.District_Name as District,(select Depotname from tbl_metadata_depot as MD Where MD.BranchID=DSC.Branch_ID) as Branch,BG_Name,SUBSTRING(SUBSTRING(SubjectName,0,CHARINDEX(',',SubjectName)), 4,Len(SubjectName)) as DSC_HOLDER_NAME,SerialNumber,SUBSTRING(SUBSTRING(IssuerName,0,CHARINDEX(',',IssuerName)), 4, Len(IssuerName)) as DSC_IssuerName,convert(varchar(10),NotAfter,103) as ValidUpto,convert(varchar(10),Created_Date,103) as DSC_UploadDate,case when User_Type='G' then 'Godown'  when User_Type='B' then 'Branch'  end User_Type from tbl_DSC_User_Upload_Detail as DSC inner join tbl_metadata_district as MD on MD.District_Id=DSC.District_Id where MD.Region_ID='" + Region_Id + "' and Verification_Status is null Order by District_name,Branch,BG_Name";

            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDSCUserVer.DataSource = ds.Tables[0];
                gvDSCUserVer.DataBind();
            }
            else
            {
                gvDSCUserVer.DataSource = "";
                gvDSCUserVer.DataBind();
            }

        }

        catch (Exception ex)
        {

        }
    }
    protected void gvDSCUserVer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string DSC_Id = "";
        string Ser_Id = "";
        if (e.CommandName == "Approve")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvDSCUserVer.Rows[rowIndex];

            if (row.RowType == DataControlRowType.DataRow)
            {
                DSC_Id = gvDSCUserVer.Rows[rowIndex].Cells[2].Text.ToString();
                Ser_Id = gvDSCUserVer.Rows[rowIndex].Cells[7].Text.ToString();
                User_Ver("Approve", DSC_Id, Ser_Id);
            }

        }
        else if (e.CommandName == "Reject")
        {

            int rowIndex = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvDSCUserVer.Rows[rowIndex];

            if (row.RowType == DataControlRowType.DataRow)
            {
                DSC_Id = gvDSCUserVer.Rows[rowIndex].Cells[2].Text.ToString();
                Ser_Id = gvDSCUserVer.Rows[rowIndex].Cells[7].Text.ToString();
                User_Ver("Reject", DSC_Id, Ser_Id);
            }
        }
    }
}
