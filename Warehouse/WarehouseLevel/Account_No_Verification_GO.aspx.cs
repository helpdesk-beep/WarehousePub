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

public partial class WarehouseLevel_Account_No_Verification_GO : System.Web.UI.Page
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
        if (Session["UserName"] != null && Session["GodownID_New"] != null)
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
    public void User_Ver(string Ver_Type, string DSCID, string Serial_No)
    {
        if ((Session["GodownID_New"] != null))
        {
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            //string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
            string Godown_Id = Session["GodownID_New"].ToString();
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
                //string str = "update tbl_DSC_User_Upload_Detail set Verification_Status='" + Ver_Type + "',Verification_Date=GETDATE(),Verified_By='" + ip + "' where Aid='" + DSCID + "' and SerialNumber='" + Serial_No + "'";
                //string str = "update tbl_Godown_Owner_Account_Details set RO_Approval_Status='" + Ver_Type + "',RO_Approval_Date=GETDATE(),RO_Approval_IP='" + ip + "' where Aid='" + DSCID + "' and Godown_Id='" + Serial_No + "'";
                string str = "update tbl_Godown_Owner_Account_Details set GO_Approval_Status='" + Ver_Type + "',GO_Approval_Date=GETDATE(),GO_Approval_IP='" + ip + "' where Aid='" + DSCID + "' and Godown_Id='" + Serial_No + "' and Godown_Id='" + Godown_Id + "'";

                cmd = new SqlCommand(str, con, sqltran);
                int req = cmd.ExecuteNonQuery();
                if (req > 0)
                {
                    sqltran.Commit();
                    con.Close();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Account Successfully Verified....')", true);

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
    private void GetDSCDetail()
    {
        try
        {
            string Godown_Id = Session["GodownID_New"].ToString();
            //string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
            //string str = "select GA.AId,DT.Regionnm,DT.District_Name,DP.DepotName,G.Godown_Name,GA.Godown_Id,G.Hired_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,(select top 1 BANK from IfscBankmar15_eUP as BM where BM.IFSC=GA.IFSC_Code) as BANK from tbl_Godown_Owner_Account_Details as GA inner join tbl_MetaData_DEPOT as DP on DP.BranchId=GA.Branch_Id inner join tbl_MetaData_DISTRICT as DT on DT.District_Id=DP.DistrictId inner join tbl_MetaData_GODOWN_2018 as G on G.Godown_ID=GA.Godown_Id where G.Godown_ID='" + Godown_Id + "' and GA.RO_Approval_Status is not NULL and (GA.GO_Approval_Status='' or GA.GO_Approval_Status is null) order by DT.Regionnm,DT.District_Name,DP.DepotName,G.Godown_Name";
            string str = "select Aid,Godown_Id,Bank_District_Id,ACCNO.Bank_Id ,Bank_Branch_Id, Acc_Holder_Name,Account_No,IFSC_Code, (select Godown_Name from tbl_MetaData_GODOWN_2018 as MG where MG.Godown_ID=ACCNO.Godown_Id) as Godown_Name,(select District_Name from tbl_MetaData_DISTRICT as MDD where MDD.District_Id=ACCNO.District_Id ) as DistrictName,BranchName,BankName  from tbl_Godown_Owner_Account_Details as ACCNO  left join  (select distinct BANK as BankName,BANK_ID from IfscBankmar15_eUP ) as BNK on BNK.BANK_ID=ACCNO.Bank_Id left join (select distinct BRANCH as BranchName ,BRANCH_ID from IfscBankmar15_eUP ) as BNKB on BNKB.BRANCH_ID=ACCNO.Bank_Branch_Id  where ACCNO.Godown_Id ='" + Godown_Id + "' and ACCNO.Godown_Id in (select distinct GS.Godown_Id from tbl_Godown_Owner_Account_Details as GS where GS.Godown_Id='" + Godown_Id + "' and GS.RO_Approval_Status is not NULL and (GS.GO_Approval_Status='' or GS.GO_Approval_Status is null)) order by Godown_Name";

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
                DSC_Id = gvDSCUserVer.Rows[rowIndex].Cells[1].Text.ToString();
                Ser_Id = gvDSCUserVer.Rows[rowIndex].Cells[2].Text.ToString();
                //User_Ver("Approve", DSC_Id, Ser_Id);
                User_Ver("Y", DSC_Id, Ser_Id);
            }

        }
        else if (e.CommandName == "Reject")
        {

            int rowIndex = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvDSCUserVer.Rows[rowIndex];

            if (row.RowType == DataControlRowType.DataRow)
            {
                DSC_Id = gvDSCUserVer.Rows[rowIndex].Cells[1].Text.ToString();
                Ser_Id = gvDSCUserVer.Rows[rowIndex].Cells[6].Text.ToString();
                //User_Ver("Reject", DSC_Id, Ser_Id);
                User_Ver("N", DSC_Id, Ser_Id);
            }
        }
    }
}
