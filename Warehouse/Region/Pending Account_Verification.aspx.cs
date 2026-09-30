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

public partial class Region_Pending_Account_Verification : System.Web.UI.Page
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
    public void User_Ver(string Ver_Type, string DSCID, string Serial_No)
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
                //string str = "update tbl_DSC_User_Upload_Detail set Verification_Status='" + Ver_Type + "',Verification_Date=GETDATE(),Verified_By='" + ip + "' where Aid='" + DSCID + "' and SerialNumber='" + Serial_No + "'";
                string str = "update tbl_Godown_Owner_Account_Details set RO_Approval_Status='" + Ver_Type + "',RO_Approval_Date=GETDATE(),RO_Approval_IP='" + ip + "' where Aid='" + DSCID + "' and Godown_Id='" + Serial_No + "'";

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
            string Region_Id = Session["Region_ID"].ToString();
            //string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);

            string str = "";
            if (ddlBillNo.SelectedValue == "1")
            {
                str = "SELECT DT.District_Name,D.DepotName,GD.Godown_Name,GD.Hired_Type,GA.Acc_Holder_Name from tbl_Godown_Owner_Account_Details as GA inner join tbl_MetaData_DEPOT as D on D.BranchId=GA.Branch_Id inner join tbl_MetaData_DISTRICT as DT on DT.District_Id=D.DistrictId inner join tbl_MetaData_Region as R on R.Region_Id=D.RegionID inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID=GA.Godown_Id where GD.Hired_Type in ('Joint Venture(JV)','Silo Bags','WDRA','PVT.PEG','Tribal Scheme','Hired') and (GA.RO_Approval_Status is null or GA.RO_Approval_Status='') and Storage_Type not in ('Permanent(CAP)','Temporary(CAP)') and DT.Region_ID='" + Region_Id + "' order by DT.District_Name,D.DepotName,GD.Godown_Name";

            }
            else if (ddlBillNo.SelectedValue == "2")
            {
                str = "SELECT DT.District_Name,D.DepotName,GD.Godown_Name,GD.Hired_Type from tbl_MetaData_GODOWN_2018 as GD inner join tbl_MetaData_DEPOT as D on D.BranchId=GD.BranchID inner join tbl_MetaData_DISTRICT as DT on DT.District_Id=D.DistrictId inner join tbl_MetaData_Region as R on R.Region_Id=D.RegionID where GD.Hired_Type in ('Joint Venture(JV)','Silo Bags','WDRA','PVT.PEG','Tribal Scheme','Hired') and GD.Godown_ID not in (select Godown_ID from tbl_Godown_Owner_Account_Details) and DT.Region_ID='" + Region_Id + "' order by DT.District_Name,D.DepotName,GD.Godown_Name";

            }
            else if (ddlBillNo.SelectedValue == "3")
            {
                str = "DT.District_Name,D.DepotName,GD.Godown_Name,GD.Hired_Type,GA.Acc_Holder_Name from tbl_Godown_Owner_Account_Details as GA inner join tbl_MetaData_DEPOT as D on D.BranchId=GA.Branch_Id inner join tbl_MetaData_DISTRICT as DT on DT.District_Id=D.DistrictId inner join tbl_MetaData_Region as R on R.Region_Id=D.RegionID inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID=GA.Godown_Id where GD.Hired_Type in ('Joint Venture(JV)','Silo Bags','WDRA','PVT.PEG','Tribal Scheme') and (GA.GO_Approval_Status is null or GA.GO_Approval_Status='') and GA.RO_Approval_Status='Y' and DT.Region_ID='" + Region_Id + "' order by DT.District_Name,D.DepotName,GD.Godown_Name";

            }
            else if (ddlBillNo.SelectedValue == "4")
            {
                str = "SELECT DT.District_Name,D.DepotName,GD.Godown_Name,GD.Hired_Type,GA.Acc_Holder_Name from tbl_Godown_Owner_Account_Details as GA inner join tbl_MetaData_DEPOT as D on D.BranchId=GA.Branch_Id inner join tbl_MetaData_DISTRICT as DT on DT.District_Id=D.DistrictId inner join tbl_MetaData_Region as R on R.Region_Id=D.RegionID inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID=GA.Godown_Id where GD.Hired_Type in ('Joint Venture(JV)','Silo Bags','WDRA','PVT.PEG','Tribal Scheme','Hired') and GA.GO_Approval_Status='Y' and GA.RO_Approval_Status='Y' and GA.Account_No not in (select Account_No from tbl_Beneficiary_Account_Details) and DT.Region_ID='" + Region_Id + "' order by DT.District_Name,D.DepotName,GD.Godown_Name";

            }
            else if (ddlBillNo.SelectedValue == "5")
            {
                str = "SELECT DT.District_Name,D.DepotName,GA.Beneficiary_Name,'******'+right(Account_No,4) as Account_No ,'******'+right(IFSC_Code,4) as IFSC_Code,'******'+right(PAN,4) as PAN,GST ,Mobile,GA.Email,Adress1,Adress2,Adress_City,'******'+right(Aadhar_No,4) as Aadhar_No from tbl_Beneficiary_Account_Details as GA inner join tbl_MetaData_DEPOT as D on D.BranchId=GA.Branch_Id inner join tbl_MetaData_DISTRICT as DT on DT.District_Id=D.DistrictId inner join tbl_MetaData_Region as R on R.Region_Id=D.RegionID where GA.GO_Approval_Status='Y' and DT.District_Id='" + Region_Id + "' order by DT.District_Name,D.DepotName,Beneficiary_Name";

            }
            else if (ddlBillNo.SelectedValue == "6")
            {
                str = "SELECT DT.District_Name,D.DepotName,GA.Beneficiary_Name,'******'+right(Account_No,4) as Account_No ,'******'+right(IFSC_Code,4) as IFSC_Code,'******'+right(PAN,4) as PAN,GST ,'******'+right(Mobile,4) as Mobile,GA.Email,Adress1,Adress2,Adress_City,'******'+right(Aadhar_No,4) as Aadhar_No from tbl_Beneficiary_Account_Details as GA inner join tbl_MetaData_DEPOT as D on D.BranchId=GA.Branch_Id inner join tbl_MetaData_DISTRICT as DT on DT.District_Id=D.DistrictId inner join tbl_MetaData_Region as R on R.Region_Id=D.RegionID where GA.GO_Approval_Status is null or GA.GO_Approval_Status='' and DT.Region_ID='" + Region_Id + "' order by DT.District_Name,D.DepotName,Beneficiary_Name";

            }
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
                DSC_Id = gvDSCUserVer.Rows[rowIndex].Cells[2].Text.ToString();
                Ser_Id = gvDSCUserVer.Rows[rowIndex].Cells[7].Text.ToString();
                //User_Ver("Reject", DSC_Id, Ser_Id);
                User_Ver("N", DSC_Id, Ser_Id);
            }
        }
    }

    protected void ddlBillNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDSCDetail();
    }
}