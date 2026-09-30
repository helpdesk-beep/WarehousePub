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
//using Microsoft.Reporting.WebForms;
using System.Security.Principal;
using MPSCSC_WS;
public partial class Accounting_BO_Final_Bill_Approval_For_Vacant_Capacity : System.Web.UI.Page
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

    MPSCSC_WS.MPSCSC_InstituitionStorageBillDetails MPSCSCDemo = new MPSCSC_WS.MPSCSC_InstituitionStorageBillDetails();
    //CSMS_WS.MPSCSC_InstituitionStorageBillDetails MPSCSCDemo = new CSMS_WS.MPSCSC_InstituitionStorageBillDetails();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["BranchId"] != null))
        {
            if (!IsPostBack)
            {
                //ModalPopupExtender1.Show();
                GetBillsDetail();
                GetBillNo();
                string MoBNo = ChkMobNo_ForOTP();
                txtMobNum.Text = MoBNo;
                lblcug.Text = txtMobNum.Text;
                //string strMsg = "यहाँ सुविधा कुछ दिनों के लिए सॉफ्टवेयर में कार्य होने कारण बंद कर दी गई हैं |||";

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/Branch_Welcome.aspx';", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    protected void gvBOBillApp_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Stop submission of Bill
        SubmitBill();
        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Bill Submission has been closed....')", true);

    }
    public void SubmitBill()
    {
        if ((Session["Depot_DistID"] != null) && (Session["BranchId"] != null))
        {
            string Dist_id = Session["Depot_DistID"].ToString();
            SqlTransaction sqltran = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqltran = con.BeginTransaction();
                string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
                string Branch_ID = Session["BranchId"].ToString();
                string BillNumber = gvBOBillApp.SelectedRow.Cells[1].Text.ToString();
                //string str = "update tbl_Storage_Bill_Details set BO_Approval_Status='Y',BO_Approval_Date=getdate(),BO_Approval_IP='" + ClientIP + "' where Bill_Number='" + gvBOBillApp.SelectedRow.Cells[1].Text.ToString() + "' and Branch_Id='" + Session["BranchId"].ToString() + "' and District_Id='" + Dist_id + "'";
                //string str = "update tbl_Institution_Storage_Bill_Details set BO_Approval_Status='Y',BO_Approval_Date=getdate(),BO_Approval_IP='" + ClientIP + "' where Bill_Number='" + gvBOBillApp.SelectedRow.Cells[1].Text.ToString() + "' and Branch_Id='" + Session["BranchId"].ToString() + "' and District_Id='" + Dist_id + "'";
                //string str = "update tbl_Institution_Storage_Bill_Summary set BO_Approval_Status='Y',BO_Approval_Date=getdate(),BO_Approval_IP='" + ClientIP + "' where Bill_Number='" + gvBOBillApp.SelectedRow.Cells[1].Text.ToString() + "' and Branch_Id='" + Session["BranchId"].ToString() + "' and District_Id='" + Dist_id + "'";
                string str = "update tbl_Institution_Vacant_Capacity_Bill_Summary set BO_Approval_Status='Y',BO_Approval_Date=getdate(),BO_Approval_IP='" + ClientIP + "' where Bill_Number='" + gvBOBillApp.SelectedRow.Cells[1].Text.ToString() + "' and Branch_Id='" + Session["BranchId"].ToString() + "'";

                cmd = new SqlCommand(str, con, sqltran);
                int req = cmd.ExecuteNonQuery();

                //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                //MPSCSCDemo.EDUpdateBOApprovalInSummary(BillNumber, Dist_id, Branch_ID, ClientIP);

                if (req > 0)
                {
                    //string str2 = "update tbl_Institution_Storage_Bill_Details set BO_Approval_Status='Y',BO_Approval_Date=getdate(),BO_Approval_IP='" + ClientIP + "' where Fin_Bill_No='" + gvBOBillApp.SelectedRow.Cells[1].Text.ToString() + "' and Branch_Id='" + Session["BranchId"].ToString() + "' and District_Id='" + Dist_id + "'";
                    string str2 = "update tbl_Institution_Vacant_Capacity_Bill_Details set BO_Approval_Status='Y',BO_Approval_Date=getdate(),BO_Approval_IP='" + ClientIP + "' where Fin_Bill_No='" + gvBOBillApp.SelectedRow.Cells[1].Text.ToString() + "' and Branch_Id='" + Session["BranchId"].ToString() + "'";

                    cmd = new SqlCommand(str2, con, sqltran);
                    int req2 = cmd.ExecuteNonQuery();

                    //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                    //MPSCSCDemo.EDUpdateBOApprovalInDetails(BillNumber, Dist_id, Branch_ID, ClientIP);

                    if (req2 > 0)
                    {
                        sqltran.Commit();
                        con.Close();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Sussessfully Submit Bill ....')", true);

                        GetBillsDetail();
                    }
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
    private void GetBillsDetail()
    {
        try
        {
            string Dist_id = Session["Depot_DistID"].ToString();
            string Branch_Id = Session["BranchId"].ToString();
            //string Dist_id = Session["Depot_DistID"].ToString();
            //string str = "select DT.District_Name,DP.DepotName,SB.Commodity_Rate,SB.Bill_Number,(select D.Depositor_Name from tbl_MetaData_DEPOSITOR as D where D.Depositor_ID=SB.Depositor_Id) as Depositor_Name,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=SB.Commodity_Id)+' '+SB.Crop_Year+'' as Commodity,CONVERT(varchar(10),SB.Created_Date,103) as Billing_Date,Floor(SB.Sub_Amount) as Charges_Amount,Floor(isnull(SB.Service_Tax_Amt,0)) as GST_AMT,Floor(isnull(SB.MPWLC_SC,0)) as Sup_Charges_Amt,Floor(isnull(SB.GST_Amt_SC,0))as GST_Sup_Amt,Floor(SB.Net_Amount) as Net_Amount,DateName( month , DateAdd( month ,SB.Month , 0 ) - 1 )+'-'+SB.Year+'' as Bill_Month,SB.Crop_Year,(select GST.GST_No from tbl_MetaData_GST_Number as GST where DT.Region_ID=GST.Region_Id) as GST,(select PN.PAN from tbl_MetaData_GST_Number as PN where DT.Region_ID=PN.Region_Id) as PAN from tbl_Institution_Storage_Bill_Summary as SB inner join tbl_MetaData_DEPOT as dp on dp.BranchId=SB.Branch_Id inner join tbl_MetaData_DISTRICT as DT on DT.District_Id=SB.District_Id  where SB.Created_Date>=CONVERT(varchar(10),'10/01/2019',101) and SB.Branch_Id='" + Branch_Id + "' and SB.District_Id='" + Dist_id + "' and BO_Approval_Status is null and SB.Depositor_Id='129'";
            //string str = "select DT.District_Name,DP.DepotName,SB.Commodity_Rate,SB.Bill_Number,(select D.Depositor_Name from tbl_MetaData_DEPOSITOR as D where D.Depositor_ID=SB.Depositor_Id) as Depositor_Name,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=SB.Commodity_Id)+' '+SB.Crop_Year+'' as Commodity,CONVERT(varchar(10),SB.Created_Date,103) as Billing_Date,Floor(SB.Sub_Amount) as Charges_Amount,Floor(isnull(SB.Service_Tax_Amt,0)) as GST_AMT,Floor(isnull(SB.MPWLC_SC,0)) as Sup_Charges_Amt,Floor(isnull(SB.GST_Amt_SC,0))as GST_Sup_Amt,Floor(SB.Net_Amount) as Net_Amount,DateName( month , DateAdd( month ,SB.Month , 0 ) - 1 )+'-'+SB.Year+'' as Bill_Month,SB.Crop_Year,(select GST.GST_No from tbl_MetaData_GST_Number as GST where DT.Region_ID=GST.Region_Id) as GST,(select PN.PAN from tbl_MetaData_GST_Number as PN where DT.Region_ID=PN.Region_Id) as PAN from tbl_Institution_Storage_Bill_Summary as SB inner join tbl_MetaData_DEPOT as dp on dp.BranchId=SB.Branch_Id inner join tbl_MetaData_DISTRICT as DT on DT.District_Id=SB.District_Id  where SB.Created_Date>=CONVERT(varchar(10),'10/01/2019',101) and SB.Branch_Id='" + Branch_Id + "' and SB.District_Id='" + Dist_id + "' and BO_Approval_Status is null and SB.Depositor_Id='129' and SB.Bill_Number in (select Bill_Number from tbl_Digitally_Signed_Bill_BO where Branch_Id='" + Branch_Id + "' and DistrictId='" + Dist_id + "') and Bill_Number in (select distinct B.Fin_Bill_No from tbl_Digitally_Signed_Bill_Details as A inner join tbl_Institution_Storage_Bill_Details as B on B.Bill_Number=A.Bill_Number where B.Branch_Id='" + Branch_Id + "' and B.District_Id='" + Dist_id + "')";
            //string str = "select DT.District_Name,DP.DepotName,SB.Commodity_Rate,SB.Bill_Number,(select D.Depositor_Name from tbl_MetaData_DEPOSITOR as D where D.Depositor_ID=SB.Depositor_Id) as Depositor_Name,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=SB.Commodity_Id)+' '+SB.Crop_Year+'' as Commodity,CONVERT(varchar(10),SB.Created_Date,103) as Billing_Date,Floor(SB.Sub_Amount) as Charges_Amount,Floor(isnull(SB.Service_Tax_Amt,0)) as GST_AMT,Floor(isnull(SB.MPWLC_SC,0)) as Sup_Charges_Amt,Floor(isnull(SB.GST_Amt_SC,0))as GST_Sup_Amt,Floor(SB.Net_Amount) as Net_Amount,DateName( month , DateAdd( month ,SB.Month , 0 ) - 1 )+'-'+SB.Year+'' as Bill_Month,SB.Crop_Year,(select GST.GST_No from tbl_MetaData_GST_Number as GST where DT.Region_ID=GST.Region_Id) as GST,(select PN.PAN from tbl_MetaData_GST_Number as PN where DT.Region_ID=PN.Region_Id) as PAN from tbl_Institution_Storage_Bill_Summary as SB inner join tbl_MetaData_DEPOT as dp on dp.BranchId=SB.Branch_Id inner join tbl_MetaData_DISTRICT as DT on DT.District_Id=SB.District_Id  where SB.Created_Date>=CONVERT(varchar(10),'10/01/2019',101) and SB.Branch_Id='" + Branch_Id + "' and SB.District_Id='" + Dist_id + "' and BO_Approval_Status is null and SB.Depositor_Id='129' and SB.Bill_Number in (select Bill_Number from tbl_Digitally_Signed_Bill_BO where Branch_Id='" + Branch_Id + "' and DistrictId='" + Dist_id + "') and Bill_Number in (select distinct B.Fin_Bill_No from tbl_Digitally_Signed_Bill_Details as A inner join tbl_Institution_Storage_Bill_Details as B on B.Bill_Number=A.Ref_Bill_No where B.Branch_Id='" + Branch_Id + "' and B.District_Id='" + Dist_id + "')";

            SqlCommand cmd = new SqlCommand("[dbo].[Get_Final_Bill_Details_For_Vacant_Capacity]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", Branch_Id.ToString());
            //cmd.Parameters.AddWithValue("@DistrictID", Dist_id.ToString());
            cmd.Parameters.AddWithValue("@FinalBillNo", ddlBillNo.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                gvBOBillApp.DataSource = dt;
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

    public void GetBillNo()
    {
        string Dist_id = Session["Depot_DistID"].ToString();
        string Branch_Id = Session["BranchId"].ToString();
        string qry = "";
        //qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Summary where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "'";
        //qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Summary where Branch_Id='" + Branch_Id + "'";
        qry = "select distinct Bill_Number from tbl_Institution_Vacant_Capacity_Bill_Summary where Branch_Id='" + Branch_Id + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBillNo.DataSource = ds.Tables[0];
            ddlBillNo.DataTextField = "Bill_Number";
            ddlBillNo.DataValueField = "Bill_Number";
            ddlBillNo.DataBind();
            //ddlBillNo.Items.Insert(0, "--Select--");
            ddlBillNo.Items.Insert(0, new ListItem("--All--", "0"));
        }
        else
        {

        }
    }

    protected void ddlBillNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBillsDetail();
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
            SubmitBill();
        }
    }
}