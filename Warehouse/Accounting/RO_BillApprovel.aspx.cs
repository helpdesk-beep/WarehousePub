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

public partial class Accounting_RO_BillApprovel : System.Web.UI.Page
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
        if ((Session["Region_ID"] != null))
        {
            if (!IsPostBack)
            {
                //ModalPopupExtender1.Show();
                GetBillsDetail();
                //string MoBNo = ChkMobNo_ForOTP();
                string MoBNo = "";
                txtMobNum.Text = MoBNo;
                lblcug.Text = txtMobNum.Text;
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    protected void gvBOBillApp_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
        btnOTP.Enabled = true;
        txtCheckOTP.Text = "";

        //SubmitBill();
    }
    public void SubmitBill()
    {
        if ((Session["Depot_DistID"] != null) && (Session["BranchId"] != null))
        {
            string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
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
                string str = "update tbl_Storage_Bill_Details set BO_Approval_Status='Y',BO_Approval_Date=getdate(),BO_Approval_IP='" + ClientIP + "' where Bill_Number='" + gvBOBillApp.SelectedRow.Cells[1].Text.ToString() + "' and Branch_Id='" + Session["BranchId"].ToString() + "' and District_Id='" + Dist_id + "'";

                cmd = new SqlCommand(str, con, sqltran);
                int req = cmd.ExecuteNonQuery();
                if (req > 0)
                {
                    sqltran.Commit();
                    con.Close();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Sussessfully Submit Bill ....')", true);

                    GetBillsDetail();
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
           // string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
            //string str = "select SB.Bill_Number,(select D.Depositor_Name from tbl_MetaData_DEPOSITOR as D where D.Depositor_ID=SB.Depositor_Id) as Depositor_Name,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=SB.Commodity_Id) as Commodity,CONVERT(varchar(10),SB.Created_Date,103) as Billing_Date,SB.Net_Amount,case when SB.Bill_Type='AU' then '' else CONVERT(varchar(10),SB.[From_Date],103) end as [From_Date],case when SB.Bill_Type='AU' then '' else CONVERT(varchar(10),SB.[To_Date],103) end as [To_Date],case when SB.Bill_Type='AD' then 'Daily Storage Charges Bill' when SB.Bill_Type='AU' then 'Accrued Storage Charges Bill' when SB.Bill_Type='OD' then 'Daily Over & Above Storage Charges Bill' when SB.Bill_Type='OU' then 'Accrued Over & Above Storage Charges Bill' when SB.Bill_Type='HG' then 'Godown(Hired) Rent Bill' when SB.Bill_Type='RB' then 'Reservation Bill' when SB.Bill_Type='GR' then 'Godown(JVS) Rent Bill' else '' end as Bill_Name from tbl_Storage_Bill_Details as SB where SB.Created_Date>=CONVERT(varchar(10),'11/01/2016',101) and SB.Branch_Id='" + Session["BranchId"].ToString() + "' and SB.District_Id='" + Dist_id + "' and BO_Approval_Status is null order by SB.Created_Date";
            string str = "select SB.Bill_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=SB.Commodity_Id) as Commodity,(select DATENAME(MONTH,GETDATE())) as Bill_Of_Month,CONVERT(varchar(10),SB.Created_Date,103) as Billing_Date,SB.Net_Amount,'Godown(JVS) Rent Bill' as Bill_Name from tbl_Storage_Bill_Details as SB inner join tbl_MetaData_DISTRICT as Dt on Dt.District_Id='23'+SB.District_Id where SB.Created_Date>=CONVERT(varchar(10),'11/01/2016',101) and BO_Approval_Status is null and Dt.Region_ID='" + Session["Region_ID"].ToString() + "' order by SB.Created_Date";

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
            SubmitBill();
        }
    }
}
