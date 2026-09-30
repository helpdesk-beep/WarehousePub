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
using Microsoft.Reporting.WebForms;
using System.Security.Principal;
public partial class E_WHR_e_WHR_Submission_S2022_23 : System.Web.UI.Page
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
        if ((Session["Depot_DistID"] != null) && (Session["BranchId"] != null))
        {
            if (!IsPostBack)
            {
                //ModalPopupExtender1.Show();
                //GetBillsDetail();
                //string MoBNo = ChkMobNo_ForOTP();
                //txtMobNum.Text = MoBNo;
                //lblcug.Text = txtMobNum.Text;
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    protected void gvBOBillApp_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ModalPopupExtender1.Show();
        //btnOTP.Enabled = true;
        //txtCheckOTP.Text = "";

        SubmitBill();
    }
    public void SubmitBill()
    {
        if ((Session["Depot_DistID"] != null) && (Session["BranchId"] != null))
        {
            string Dist_id = Session["Depot_DistID"].ToString();
            string BranchId = Session["BranchId"].ToString();
            string User_Type = gvBOBillApp.SelectedRow.Cells[11].Text.ToString();
            string str = "";
            string GodownId = gvBOBillApp.SelectedRow.Cells[2].Text.ToString();
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
                if (User_Type == "B")
                {
                    //str = "INSERT INTO [tbl_DSC_eWHR_Submission](DistrictId,BranchId,[WHR_Id],[Godown_Id],Commodity_Id,[BSerial_No],[BSigning_Person],[BSigning_Date],[BSigning_Ip],[eWHR_Bags],[eWHR_Qty],[Date_of_Deposit],[User_Type],[CreatedDate],[CreatedBy],PrintedDate,PrintedIp)VALUES ('" + Dist_id + "','" + BranchId + "','" + gvBOBillApp.SelectedRow.Cells[3].Text.ToString() + "','" + gvBOBillApp.SelectedRow.Cells[2].Text.ToString() + "','" + gvBOBillApp.SelectedRow.Cells[12].Text.ToString() + "','" + gvBOBillApp.SelectedRow.Cells[5].Text.ToString() + "','" + gvBOBillApp.SelectedRow.Cells[4].Text.ToString() + "','" + gvBOBillApp.SelectedRow.Cells[6].Text.ToString() + "','" + gvBOBillApp.SelectedRow.Cells[7].Text.ToString() + "','" + gvBOBillApp.SelectedRow.Cells[8].Text.ToString() + "','" + gvBOBillApp.SelectedRow.Cells[9].Text.ToString() + "','" + gvBOBillApp.SelectedRow.Cells[10].Text.ToString() + "','" + gvBOBillApp.SelectedRow.Cells[11].Text.ToString() + "',GETDATE(),'" + ClientIP + "','','')";
                    str = "INSERT INTO [tbl_DSC_eWHR_Submission_CMS2023](DistrictId,BranchId,[WHR_Id],[Godown_Id],Commodity_Id,[BSerial_No],[BSigning_Person],[BSigning_Date],[BSigning_Ip],[eWHR_Bags],[eWHR_Qty],[Date_of_Deposit],[User_Type],[CreatedDate],[CreatedBy],PrintedDate,PrintedIp)VALUES ('" + Dist_id + "','" + BranchId + "','" + gvBOBillApp.SelectedRow.Cells[3].Text.ToString() + "','" + gvBOBillApp.SelectedRow.Cells[2].Text.ToString() + "','" + gvBOBillApp.SelectedRow.Cells[12].Text.ToString() + "','" + gvBOBillApp.SelectedRow.Cells[5].Text.ToString() + "','" + gvBOBillApp.SelectedRow.Cells[4].Text.ToString() + "','" + gvBOBillApp.SelectedRow.Cells[6].Text.ToString() + "','" + gvBOBillApp.SelectedRow.Cells[7].Text.ToString() + "','" + gvBOBillApp.SelectedRow.Cells[8].Text.ToString() + "','" + gvBOBillApp.SelectedRow.Cells[9].Text.ToString() + "','" + gvBOBillApp.SelectedRow.Cells[10].Text.ToString() + "','" + gvBOBillApp.SelectedRow.Cells[11].Text.ToString() + "',GETDATE(),'" + ClientIP + "','','')";

                }
                else if (User_Type == "G")
                {
                    //str = " update [tbl_DSC_eWHR_Submission] set [GSerial_No]='" + gvBOBillApp.SelectedRow.Cells[5].Text.ToString() + "',[GSigning_Person]='" + gvBOBillApp.SelectedRow.Cells[4].Text.ToString() + "',[GSigning_Date]='" + gvBOBillApp.SelectedRow.Cells[6].Text.ToString() + "',[GSigning_Ip]='" + gvBOBillApp.SelectedRow.Cells[7].Text.ToString() + "',[GUser_Type]='" + gvBOBillApp.SelectedRow.Cells[11].Text.ToString() + "',[GCreatedDate]=GETDATE(),[GCreatedBy]='" + ClientIP + "', GWHR_Id='" + gvBOBillApp.SelectedRow.Cells[3].Text.ToString() + "' where [BranchId]='" + BranchId + "' and [WHR_Id]='" + gvBOBillApp.SelectedRow.Cells[3].Text.ToString() + "' and [Godown_Id]='" + GodownId + "'";
                    str = " update [tbl_DSC_eWHR_Submission_CMS2023] set [GSerial_No]='" + gvBOBillApp.SelectedRow.Cells[5].Text.ToString() + "',[GSigning_Person]='" + gvBOBillApp.SelectedRow.Cells[4].Text.ToString() + "',[GSigning_Date]='" + gvBOBillApp.SelectedRow.Cells[6].Text.ToString() + "',[GSigning_Ip]='" + gvBOBillApp.SelectedRow.Cells[7].Text.ToString() + "',[GUser_Type]='" + gvBOBillApp.SelectedRow.Cells[11].Text.ToString() + "',[GCreatedDate]=GETDATE(),[GCreatedBy]='" + ClientIP + "', GWHR_Id='" + gvBOBillApp.SelectedRow.Cells[3].Text.ToString() + "' where [BranchId]='" + BranchId + "' and [WHR_Id]='" + gvBOBillApp.SelectedRow.Cells[3].Text.ToString() + "' and [Godown_Id]='" + GodownId + "'";

                }
                cmd = new SqlCommand(str, con, sqltran);
                int req = cmd.ExecuteNonQuery();
                if (req > 0)
                {
                    sqltran.Commit();
                    con.Close();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Sussessfully Submit e WHR ....')", true);

                    GetBillsDetail(User_Type);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('पहले शाखा प्रबंधक द्वारा साइन e WHR Submit करे ....')", true);
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
    private void GetBillsDetail(string UserType)
    {
        try
        {
            string str = "";
            string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
            //string str = "select GD.Godown_Name as Godown,GD.Godown_ID,DSWHR.Depositor_WHR_Id as WHR_Id,DSWHR.DSC_Holder_Name as Signing_Person,DSWHR.DSC_Serial_No as Serial_No,DSWHR.CreatedDate as Signing_Date,DSWHR.Client_Ip as Signing_Ip,DSWHR.TotalBags_Received as Bags,DSWHR.Total_Qty_Received as Qty,CONVERT(varchar(10),DSWHR.Date_of_Deposit,103) as Date_of_Deposit,DSWHR.DSC_User_Type as User_Type,Commodity_Id from tbl_Digitally_Signed_WHR_Details as DSWHR inner join tbl_Digital_Signature_Details as DSign on DSign.WHR_No=DSWHR.Depositor_WHR_Id and DSign.DSC_Serial_No=DSWHR.DSC_Serial_No inner join tbl_DSC_WHR_XML_File as DXML on DXML.WHR_Id=DSWHR.Depositor_WHR_Id and DXML.DSC_Serial_No=DSWHR.DSC_Serial_No inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID=DSWHR.GodownID where DSWHR.BranchID='" + Session["BranchId"].ToString() + "' and DSWHR.District_Id='" + Session["Depot_DistID"].ToString() + "' and DSWHR.DSC_Holder_Name is not null and DSWHR.CreatedDate  is not null and DSWHR.Client_Ip is not null and DSWHR.Client_Ip!='' and DSWHR.DSC_User_Type='B' and DSWHR.Depositor_WHR_Id not in (select distinct A.WHR_Id from tbl_DSC_eWHR_Submission as A where A.BranchId='" + Session["BranchId"].ToString() + "')";
            //if (UserType == "B" && ddlCommodity.SelectedValue == "W")
            //{
            //    str = "select GD.Godown_Name as Godown,GD.Godown_ID,DSWHR.Depositor_WHR_Id as WHR_Id,DSWHR.DSC_Holder_Name as Signing_Person,DSWHR.DSC_Serial_No as Serial_No,DSWHR.CreatedDate as Signing_Date,DSWHR.Client_Ip as Signing_Ip,DSWHR.TotalBags_Received as Bags,DSWHR.Total_Qty_Received as Qty,CONVERT(varchar(10),DSWHR.Date_of_Deposit,103) as Date_of_Deposit,DSWHR.DSC_User_Type as User_Type,Commodity_Id from tbl_Digitally_Signed_WHR_Wheat2019 as DSWHR inner join tbl_Digital_Signature_Wheat2019 as DSign on DSign.WHR_No=DSWHR.Depositor_WHR_Id and DSign.DSC_Serial_No=DSWHR.DSC_Serial_No inner join tbl_DSC_WHR_XML_File_Wheat2019 as DXML on DXML.WHR_Id=DSWHR.Depositor_WHR_Id and DXML.DSC_Serial_No=DSWHR.DSC_Serial_No inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID=DSWHR.GodownID where DSWHR.BranchID='" + Session["BranchId"].ToString() + "' and DSWHR.District_Id='" + Session["Depot_DistID"].ToString() + "' and DSWHR.DSC_Holder_Name!='' and DSWHR.CreatedDate!='' and (DSWHR.Client_Ip!='' or DSWHR.CreatedBy!='') and DSWHR.DSC_User_Type='B' and DSWHR.Depositor_WHR_Id not in (select distinct A.WHR_Id from tbl_DSC_eWHR_Submission as A where A.BranchId='" + Session["BranchId"].ToString() + "' and DSWHR.Depositor_WHR_Id in (select Depositor_WHR_Id from View_Digitally_Signed_WHR_Wheat2019))";
            //}
            //else if (UserType == "G" && ddlCommodity.SelectedValue == "W")
            //{
            //    str = "select GD.Godown_Name as Godown,GD.Godown_ID,DSWHR.Depositor_WHR_Id as WHR_Id,DSWHR.DSC_Holder_Name as Signing_Person,DSWHR.DSC_Serial_No as Serial_No,DSWHR.CreatedDate as Signing_Date,DSWHR.Client_Ip as Signing_Ip,DSWHR.TotalBags_Received as Bags,DSWHR.Total_Qty_Received as Qty,CONVERT(varchar(10),DSWHR.Date_of_Deposit,103) as Date_of_Deposit,DSWHR.DSC_User_Type as User_Type,Commodity_Id from tbl_Digitally_Signed_WHR_Wheat2019 as DSWHR inner join tbl_Digital_Signature_Wheat2019 as DSign on DSign.WHR_No=DSWHR.Depositor_WHR_Id and DSign.DSC_Serial_No=DSWHR.DSC_Serial_No inner join tbl_DSC_WHR_XML_File_Wheat2019 as DXML on DXML.WHR_Id=DSWHR.Depositor_WHR_Id and DXML.DSC_Serial_No=DSWHR.DSC_Serial_No inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID=DSWHR.GodownID where DSWHR.BranchID='" + Session["BranchId"].ToString() + "' and DSWHR.District_Id='" + Session["Depot_DistID"].ToString() + "' and DSWHR.DSC_Holder_Name!='' and DSWHR.CreatedDate!='' and (DSWHR.Client_Ip!='' or DSWHR.CreatedBy!='') and DSWHR.DSC_User_Type='G' and DSWHR.Depositor_WHR_Id not in (select distinct A.WHR_Id from tbl_DSC_eWHR_Submission as A where A.BranchId='" + Session["BranchId"].ToString() + "' and DSWHR.Depositor_WHR_Id in (select Depositor_WHR_Id from View_Digitally_Signed_WHR_Wheat2019))";
            //}
            if (UserType == "B" && ddlCommodity.SelectedValue == "D")
            {
                //str = "select GD.Godown_Name as Godown,GD.Godown_ID,DSWHR.Depositor_WHR_Id as WHR_Id,DSWHR.DSC_Holder_Name as Signing_Person,DSWHR.DSC_Serial_No as Serial_No,DSWHR.CreatedDate as Signing_Date,DSWHR.Client_Ip as Signing_Ip,DSWHR.TotalBags_Received as Bags,DSWHR.Total_Qty_Received as Qty,CONVERT(varchar(10),DSWHR.Date_of_Deposit,103) as Date_of_Deposit,DSWHR.DSC_User_Type as User_Type,Commodity_Id from tbl_Digitally_Signed_WHR_CMS2020 as DSWHR inner join tbl_Digital_Signature_CMS2020 as DSign on DSign.WHR_No=DSWHR.Depositor_WHR_Id and DSign.DSC_Serial_No=DSWHR.DSC_Serial_No inner join tbl_DSC_WHR_XML_File_CMS2020 as DXML on DXML.WHR_Id=DSWHR.Depositor_WHR_Id and DXML.DSC_Serial_No=DSWHR.DSC_Serial_No inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID=DSWHR.GodownID where DSWHR.BranchID='" + Session["BranchId"].ToString() + "' and DSWHR.District_Id='" + Session["Depot_DistID"].ToString() + "' and DSWHR.DSC_Holder_Name!='' and DSWHR.CreatedDate!='' and (DSWHR.Client_Ip!='' or DSWHR.CreatedBy!='') and DSWHR.DSC_User_Type='B' and DSWHR.Depositor_WHR_Id not in (select distinct A.WHR_Id from tbl_DSC_eWHR_Submission_CMS2020 as A where A.BranchId='" + Session["BranchId"].ToString() + "' and DSWHR.Depositor_WHR_Id in (select Depositor_WHR_Id from View_Digitally_Signed_WHR_CMS2020))";
                //str = "select GD.Godown_Name as Godown,GD.Godown_ID,DSWHR.Depositor_WHR_Id as WHR_Id,DSWHR.DSC_Holder_Name as Signing_Person,DSWHR.DSC_Serial_No as Serial_No,DSWHR.CreatedDate as Signing_Date,DSWHR.Client_Ip as Signing_Ip,DSWHR.TotalBags_Received as Bags,DSWHR.Total_Qty_Received as Qty,CONVERT(varchar(10),DSWHR.Date_of_Deposit,103) as Date_of_Deposit,DSWHR.DSC_User_Type as User_Type,Commodity_Id from tbl_Digitally_Signed_WHR_CMS2022 as DSWHR inner join tbl_Digital_Signature_CMS2022 as DSign on DSign.WHR_No=DSWHR.Depositor_WHR_Id and DSign.DSC_Serial_No=DSWHR.DSC_Serial_No inner join tbl_DSC_WHR_XML_File_CMS2022 as DXML on DXML.WHR_Id=DSWHR.Depositor_WHR_Id and DXML.DSC_Serial_No=DSWHR.DSC_Serial_No inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID=DSWHR.GodownID where DSWHR.BranchID='" + Session["BranchId"].ToString() + "' and DSWHR.District_Id='" + Session["Depot_DistID"].ToString() + "' and DSWHR.DSC_Holder_Name!='' and DSWHR.CreatedDate!='' and (DSWHR.Client_Ip!='' or DSWHR.CreatedBy!='') and DSWHR.DSC_User_Type='B' and DSWHR.Depositor_WHR_Id not in (select distinct A.WHR_Id from tbl_DSC_eWHR_Submission_CMS2022 as A where A.BranchId='" + Session["BranchId"].ToString() + "' and DSWHR.Depositor_WHR_Id in (select Depositor_WHR_Id from View_Digitally_Signed_WHR_CMS2022)) and DSWHR.DepositorID='10535'";
                str = "select GD.Godown_Name as Godown,GD.Godown_ID,DSWHR.Depositor_WHR_Id as WHR_Id,DSWHR.DSC_Holder_Name as Signing_Person,DSWHR.DSC_Serial_No as Serial_No,DSWHR.CreatedDate as Signing_Date,DSWHR.Client_Ip as Signing_Ip,DSWHR.TotalBags_Received as Bags,DSWHR.Total_Qty_Received as Qty,CONVERT(varchar(10),DSWHR.Date_of_Deposit,103) as Date_of_Deposit,DSWHR.DSC_User_Type as User_Type,Commodity_Id from tbl_Digitally_Signed_WHR_CMS2023 as DSWHR inner join tbl_Digital_Signature_CMS2023 as DSign on DSign.WHR_No=DSWHR.Depositor_WHR_Id and DSign.DSC_Serial_No=DSWHR.DSC_Serial_No inner join tbl_DSC_WHR_XML_File_CMS2023 as DXML on DXML.WHR_Id=DSWHR.Depositor_WHR_Id and DXML.DSC_Serial_No=DSWHR.DSC_Serial_No inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID=DSWHR.GodownID where DSWHR.BranchID='" + Session["BranchId"].ToString() + "' and DSWHR.District_Id='" + Session["Depot_DistID"].ToString() + "' and DSWHR.DSC_Holder_Name!='' and DSWHR.CreatedDate!='' and (DSWHR.Client_Ip!='' or DSWHR.CreatedBy!='') and DSWHR.DSC_User_Type='B' and DSWHR.Depositor_WHR_Id not in (select distinct A.WHR_Id from tbl_DSC_eWHR_Submission_CMS2023 as A where A.BranchId='" + Session["BranchId"].ToString() + "' and DSWHR.Depositor_WHR_Id in (select Depositor_WHR_Id from View_Digitally_Signed_WHR_CMS2023))";

            }
            else if (UserType == "G" && ddlCommodity.SelectedValue == "D")
            {
                //str = "select GD.Godown_Name as Godown,GD.Godown_ID,DSWHR.Depositor_WHR_Id as WHR_Id,DSWHR.DSC_Holder_Name as Signing_Person,DSWHR.DSC_Serial_No as Serial_No,DSWHR.CreatedDate as Signing_Date,DSWHR.Client_Ip as Signing_Ip,DSWHR.TotalBags_Received as Bags,DSWHR.Total_Qty_Received as Qty,CONVERT(varchar(10),DSWHR.Date_of_Deposit,103) as Date_of_Deposit,DSWHR.DSC_User_Type as User_Type,Commodity_Id from tbl_Digitally_Signed_WHR_CMS2020 as DSWHR inner join tbl_Digital_Signature_CMS2020 as DSign on DSign.WHR_No=DSWHR.Depositor_WHR_Id and DSign.DSC_Serial_No=DSWHR.DSC_Serial_No inner join tbl_DSC_WHR_XML_File_CMS2020 as DXML on DXML.WHR_Id=DSWHR.Depositor_WHR_Id and DXML.DSC_Serial_No=DSWHR.DSC_Serial_No inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID=DSWHR.GodownID where DSWHR.BranchID='" + Session["BranchId"].ToString() + "' and DSWHR.District_Id='" + Session["Depot_DistID"].ToString() + "' and DSWHR.DSC_Holder_Name!='' and DSWHR.CreatedDate!='' and (DSWHR.Client_Ip!='' or DSWHR.CreatedBy!='') and DSWHR.DSC_User_Type='G' and DSWHR.Depositor_WHR_Id not in (select distinct ISNULL(A.GWHR_Id,0) from tbl_DSC_eWHR_Submission_CMS2020 as A where A.BranchId='" + Session["BranchId"].ToString() + "' and DSWHR.Depositor_WHR_Id in (select Depositor_WHR_Id from View_Digitally_Signed_WHR_CMS2020 where User_Type='G'))";
                //str = "select GD.Godown_Name as Godown,GD.Godown_ID,DSWHR.Depositor_WHR_Id as WHR_Id,DSWHR.DSC_Holder_Name as Signing_Person,DSWHR.DSC_Serial_No as Serial_No,DSWHR.CreatedDate as Signing_Date,DSWHR.Client_Ip as Signing_Ip,DSWHR.TotalBags_Received as Bags,DSWHR.Total_Qty_Received as Qty,CONVERT(varchar(10),DSWHR.Date_of_Deposit,103) as Date_of_Deposit,DSWHR.DSC_User_Type as User_Type,Commodity_Id from tbl_Digitally_Signed_WHR_CMS2022 as DSWHR inner join tbl_Digital_Signature_CMS2022 as DSign on DSign.WHR_No=DSWHR.Depositor_WHR_Id and DSign.DSC_Serial_No=DSWHR.DSC_Serial_No inner join tbl_DSC_WHR_XML_File_CMS2022 as DXML on DXML.WHR_Id=DSWHR.Depositor_WHR_Id and DXML.DSC_Serial_No=DSWHR.DSC_Serial_No inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID=DSWHR.GodownID where DSWHR.BranchID='" + Session["BranchId"].ToString() + "' and DSWHR.District_Id='" + Session["Depot_DistID"].ToString() + "' and DSWHR.DSC_Holder_Name!='' and DSWHR.CreatedDate!='' and (DSWHR.Client_Ip!='' or DSWHR.CreatedBy!='') and DSWHR.DSC_User_Type='G' and DSWHR.Depositor_WHR_Id not in (select distinct ISNULL(A.GWHR_Id,0) from tbl_DSC_eWHR_Submission_CMS2022 as A where A.BranchId='" + Session["BranchId"].ToString() + "' and DSWHR.Depositor_WHR_Id in (select Depositor_WHR_Id from View_Digitally_Signed_WHR_CMS2022 where User_Type='G')) and DSWHR.DepositorID='10535'";
                str = "select GD.Godown_Name as Godown,GD.Godown_ID,DSWHR.Depositor_WHR_Id as WHR_Id,DSWHR.DSC_Holder_Name as Signing_Person,DSWHR.DSC_Serial_No as Serial_No,DSWHR.CreatedDate as Signing_Date,DSWHR.Client_Ip as Signing_Ip,DSWHR.TotalBags_Received as Bags,DSWHR.Total_Qty_Received as Qty,CONVERT(varchar(10),DSWHR.Date_of_Deposit,103) as Date_of_Deposit,DSWHR.DSC_User_Type as User_Type,Commodity_Id from tbl_Digitally_Signed_WHR_CMS2023 as DSWHR inner join tbl_Digital_Signature_CMS2023 as DSign on DSign.WHR_No=DSWHR.Depositor_WHR_Id and DSign.DSC_Serial_No=DSWHR.DSC_Serial_No inner join tbl_DSC_WHR_XML_File_CMS2023 as DXML on DXML.WHR_Id=DSWHR.Depositor_WHR_Id and DXML.DSC_Serial_No=DSWHR.DSC_Serial_No inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID=DSWHR.GodownID where DSWHR.BranchID='" + Session["BranchId"].ToString() + "' and DSWHR.District_Id='" + Session["Depot_DistID"].ToString() + "' and DSWHR.DSC_Holder_Name!='' and DSWHR.CreatedDate!='' and (DSWHR.Client_Ip!='' or DSWHR.CreatedBy!='') and DSWHR.DSC_User_Type='G' and DSWHR.Depositor_WHR_Id not in (select distinct ISNULL(A.GWHR_Id,0) from tbl_DSC_eWHR_Submission_CMS2023 as A where A.BranchId='" + Session["BranchId"].ToString() + "' and DSWHR.Depositor_WHR_Id in (select Depositor_WHR_Id from View_Digitally_Signed_WHR_CMS2023 where User_Type='G'))";

            }
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBOBillApp.DataSource = ds.Tables[0];
                gvBOBillApp.DataBind();
            }
            else
            {
                gvBOBillApp.DataSource = "";
                gvBOBillApp.DataBind();
            }

        }

        catch (Exception ex)
        {

        }
    }
    protected void GenerateUniqueOTP()
    {
        string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
        string numbers = "1234567890";

        string characters = numbers;

        characters += alphabets + small_alphabets + numbers;

        string MONumber = gvBOBillApp.SelectedRow.Cells[1].Text.ToString();
        //   string MONumber = "";
        int lastdigit = int.Parse(MONumber.Substring(6));
        int length = 0;
        if (lastdigit >= 6)
        {
            length = 8;
        }
        else if (lastdigit >= 3 && lastdigit <= 5)
        {
            length = 6;
        }
        else
        {
            length = 5;
        }

        //int length = int.Parse(ddlMvmtNo.SelectedItem.Value);
        string otp = string.Empty;
        for (int i = 0; i < length; i++)
        {
            string character = string.Empty;
            do
            {
                int index = new Random().Next(0, characters.Length);
                character = characters.ToCharArray()[index].ToString();
            } while (otp.IndexOf(character) != -1);
            otp += character;
        }

        GenerateOTP = otp;
        //  OTPSMS = "'" + ddl_commodity.SelectedItem.Text + "' Depositor Form Number " + ddl_depositerform.SelectedItem.Text + " OTP Is '" + otp + "'";
        OTPSMS = "Net Amount '" + gvBOBillApp.SelectedRow.Cells[5].Text.ToString() + "' Bill Number " + gvBOBillApp.SelectedRow.Cells[1].Text.ToString() + " OTP Is " + otp + " ";
        hdfOTP.Value = "";
        hdfOTP.Value = otp;
        SMS Message = new SMS();
        string MobileNo = "";
        MobileNo = txtMobNum.Text;
        Message.SendSMS(MobileNo, OTPSMS);
    }
    protected void btnOTP_Click(object sender, EventArgs e)
    {
        if (txtMobNum.Text != "" && lblcug.Text != "")
        {
            txtCheckOTP.Text = "";
            GenerateUniqueOTP();
            txtCheckOTP.Focus();
            trbtn.Visible = true;
            btnOTP.Enabled = false;
            //    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:TimerFunc(); ", true);
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Mobile Number Not Available'); </script> ");
            return;
        }
        ModalPopupExtender1.Show();
    }
    public string ChkMobNo_ForOTP()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string ReturnMobNo = "";
        string QueryMax = "select Mobile_No from [tbl_Warehousing_Contact] where Branch_ID='" + Session["BranchId"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(QueryMax, con);
        string str3 = cmd.ExecuteScalar().ToString();
        if ((str3 != String.Empty) || str3 != "" || str3 != "0")
        {
            ReturnMobNo = str3;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Mobile No Available For Sending OTP Please Contact HeadOffice Bhopal ....')", true);
        }
        return ReturnMobNo;
    }

    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        if (hdfOTP.Value != txtCheckOTP.Text)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Enter Correct OTP ....')", true);
            ModalPopupExtender1.Show();
            btnOTP.Enabled = false;
            txtCheckOTP.Text = "";
        }
        else if (hdfOTP.Value == txtCheckOTP.Text)
        {
            //SubmitBill();
        }
    }
    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string UT = "";
        UT = ddlUserType.SelectedValue;
        GetBillsDetail(UT);
    }
}