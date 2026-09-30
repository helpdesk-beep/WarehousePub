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

public partial class WarehouseLevel_Account_Detail_Verification_GO : System.Web.UI.Page
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
                //string str = "update tbl_Godown_Owner_Account_Details set GO_Approval_Status='" + Ver_Type + "',GO_Approval_Date=GETDATE(),GO_Approval_IP='" + ip + "' where Aid='" + DSCID + "' and Godown_Id='" + Serial_No + "' and Godown_Id='" + Godown_Id + "'";
                string str = "update tbl_Beneficiary_Account_Details set GO_Approval_Status='" + Ver_Type + "',GO_Approval_Date=GETDATE(),GO_Approval_IP='" + ip + "' where Beneficiary_Id='" + DSCID + "' and Account_No='" + Serial_No + "'";

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
            string str = " select Beneficiary_Id,Beneficiary_Name,tbl_Beneficiary_Account_Details.Account_No,tbl_Beneficiary_Account_Details.IFSC_Code,PAN,GST,Mobile,Email,Aadhar_No,Adress1+' '+Adress2+' '+Adress_City as Address from tbl_Beneficiary_Account_Details inner join tbl_Godown_Owner_Account_Details on tbl_Godown_Owner_Account_Details.Account_No=tbl_Beneficiary_Account_Details.Account_No where tbl_Beneficiary_Account_Details.GO_Approval_Status!='Y' and tbl_Godown_Owner_Account_Details.Godown_Id='"+ Godown_Id + "'";

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
                Ser_Id = gvDSCUserVer.Rows[rowIndex].Cells[3].Text.ToString();
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